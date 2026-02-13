using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

/// <summary>
/// Represents the player controller in the exploration phase
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask mask;
    [SerializeField] private PlayerInteraction interaction;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float hoverDistance = 0.01f;
    [SerializeField] private float groundCheckDistance = 0.05f;


    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform graphicToFlip;
    [SerializeField] private VisualEffect vfxWalk;
    [SerializeField] private ParticleSystem vfxClick;
    [SerializeField] private VisualEffect vfxClickGraph;

    private bool lastVfxState;

    private bool canUpdateTargetWithMouse;
    private bool moveToTarget;
    private Vector2 mousePosition;
    private Vector3 targetPosition;
    private Vector2 moveVector;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(rb.position, rb.position + Vector3.down * groundCheckDistance);
    }

    /// <summary>
    /// Gets the player body
    /// </summary>
    /// <returns></returns>
    public Rigidbody GetBody()
    {
        return rb;
    }

    /// <summary>
    /// Updates the target position
    /// </summary>
    /// <param name="mousePosition">The mouse position</param>
    /// <returns>True if succeded</returns>
    public bool UpdateTargetPosition(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, mask))
        {
            targetPosition = hitInfo.point;
            targetPosition.y = rb.position.y;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the move vector (keyboard/gamepad)
    /// </summary>
    /// <param name="moveVector">The move vector</param>
    public void SetMoveVector(Vector2 moveVector)
    {
        moveToTarget = false;
        interaction.DisableClosingInTag();
        this.moveVector = moveVector;
    }

    /// <summary>
    /// Sets the mouse position
    /// </summary>
    /// <param name="mousePosition">The mouse position</param>
    public void SetMousePosition(Vector2 mousePosition)
    {
        this.mousePosition = mousePosition;
    }

    /// <summary>
    /// Toggle if the controller should try to update the target with the mouse postion
    /// </summary>
    public void ToggleUpdateTargetWithMouse()
    {
        canUpdateTargetWithMouse = !canUpdateTargetWithMouse;
        if (canUpdateTargetWithMouse)
        {
            moveToTarget = true;
            interaction.DisableClosingInTag();
        }

    }

    /// <summary>
    /// Sets if the controller should try to update the target with the mouse postion
    /// </summary>
    /// <param name="value">True if the controller should move using the mouse</param>
    public void SetUpdateTargetWithMouse(bool value)
    {
        canUpdateTargetWithMouse = value;
        if (canUpdateTargetWithMouse)
        {
            interaction.DisableClosingInTag();
            moveToTarget = true;
        }

    }

    /// <summary>
    /// Sets the target position manually
    /// </summary>
    /// <param name="targetPosition">The new target position</param>
    public void SetTargetPosition(Vector3 targetPosition)
    {
        moveToTarget = true;
        this.targetPosition = targetPosition;
    }

    /// <summary>
    /// Sets the position of the controller
    /// </summary>
    /// <param name="position">The new position</param>
    public void SetPosition(Vector3 position)
    {
        rb.position = position;
    }


    void Update()
    {
        // Ground check

        if (Physics.Raycast(rb.position, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer))
        {
            rb.position = new Vector3(rb.position.x, hit.point.y + hoverDistance, rb.position.z);
        }


        // Walking animation

        bool isWalking = rb.linearVelocity.magnitude > 0.5f;

        animator.SetBool("isWalking", isWalking);
        vfxWalk.SetBool("isWalking", isWalking);


        // Flip to the right side

        if (isWalking)
        {
            if (moveToTarget)
            {

                Vector3 velocity = (targetPosition - rb.position).normalized;
                Vector3 s = graphicToFlip.localScale;
                s.x = Mathf.Abs(s.x) * -Mathf.Sign(velocity.x);
                graphicToFlip.localScale = s;
            }
            else if (moveVector != Vector2.zero)
            {
                Vector3 s = graphicToFlip.localScale;
                s.x = Mathf.Abs(s.x) * -Mathf.Sign(moveVector.x);
                graphicToFlip.localScale = s;
            }
        }

        if (!Player.instance.canComponentsUpdate)
        {
            target.position = new Vector3(0, -50, 0);
            return;
        }

        if (moveToTarget)
        {
            if (canUpdateTargetWithMouse)
            {
                UpdateTargetPosition(mousePosition);
            }

            Vector3 direction = targetPosition - rb.position;

            if (direction.magnitude > 0.5f)
            {
                target.position = targetPosition;
                direction = direction.normalized * playerSpeed;
                direction.y = 0;
                rb.linearVelocity = direction;
            }
            else if (!canUpdateTargetWithMouse)
            {
                target.position = new Vector3(0, -50, 0);
            }
        }
        else
        {
            target.position = new Vector3(0, -50, 0);

            if (moveVector != Vector2.zero)
            {
                rb.linearVelocity = new Vector3(moveVector.x, 0, moveVector.y) * playerSpeed;
            }
        }
    }

    public void PlayMouseTargetVFX()
    {
        if (!canUpdateTargetWithMouse)
            return;

        if (!UpdateTargetPosition(mousePosition))
            return;

        vfxClick.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        vfxClick.Play();

        vfxClickGraph.Reinit();
        vfxClickGraph.Play();
    }
}
