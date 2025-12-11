using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the player controller in the exploration phase
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask mask;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform graphicToFlip;

    private bool shouldTryToMoveUsingCursor;
    private Vector2 mousePosition;
    private Vector2 moveVector;


    /// <summary>
    /// Sets the move vector (keyboard/gamepad)
    /// </summary>
    /// <param name="moveVector">The move vector</param>
    public void SetMoveVector(Vector2 moveVector)
    {
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
    /// Toggle if the controller should try to move the player using the mouse position
    /// </summary>
    public void ToggleTryToMoveUsingCursor()
    {
        shouldTryToMoveUsingCursor = !shouldTryToMoveUsingCursor;
    }

    /// <summary>
    /// Sets if the controller should try to move the player using the mouse position
    /// </summary>
    /// <param name="value">True if the controller should move using the mouse</param>
    public void SetTryToMoveUsingCursor(bool value)
    {
        shouldTryToMoveUsingCursor = value;
    }



    void Update()
    {
        if (shouldTryToMoveUsingCursor && moveVector == Vector2.zero)
        {
            // Get exact location of click
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, mask))
            {
                Vector3 place = hitInfo.point;
                place.y = rb.position.y;

                target.position = place;

                Vector3 direction = place - rb.position;

                if (direction.magnitude > 0.5f)
                {
                    rb.linearVelocity = direction.normalized * playerSpeed;
                }
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

        // Walking animation

        bool isWalking = rb.linearVelocity.magnitude > 0.1f;

        animator.SetBool("isWalking", isWalking);

        Vector3 velocity = rb.linearVelocity;

        // Flip to the right side

        if (Mathf.Abs(velocity.x) > 0.1f)
        {
            Vector3 s = graphicToFlip.localScale;
            s.x = Mathf.Abs(s.x) * -Mathf.Sign(velocity.x);
            graphicToFlip.localScale = s;
        }
    }
}
