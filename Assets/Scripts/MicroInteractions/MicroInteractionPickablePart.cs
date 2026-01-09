using UnityEngine;

/// <summary>
/// Represents a part of a pickable object
/// A part is the actual thing that moves / rotates
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MicroInteractionPickablePart : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private MicroInteractionPickable parent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Gets the part's parent
    /// </summary>
    /// <returns>Its parent</returns>
    public MicroInteractionPickable GetParent()
    {
        return parent;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(parent.GetCurrentMovingPart() == null) return;

        if(collision.transform.tag == "Ground")
        {
            parent.InvokeOnTouchGround();
        }else if(collision.transform.tag == "Plant")
        {
            parent.InvokeOnStartTouchPlant();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(parent.GetCurrentMovingPart() == null) return;

        if(collision.transform.tag == "Plant")
        {
            parent.InvokeOnEndTouchPlant();
        }
    }

    /// <summary>
    /// Pickup the part
    /// </summary>
    /// <param name="canRotate">True if the part can rotate</param>
    public void Pickup(bool canRotate)
    {
        //SetRigidbodyGravityScale(0f);
        //SetRigidbodyType(RigidbodyType2D.Kinematic);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.freezeRotation = !canRotate;
    }

    /// <summary>
    /// Drop the part
    /// </summary>
    public void Drop()
    {
        //SetRigidbodyGravityScale(1f);
        //SetRigidbodyType(RigidbodyType2D.Dynamic);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        rb.freezeRotation = false;
    }

    /// <summary>
    /// Moves the part towards a position
    /// </summary>
    /// <param name="position">The position</param>
    /// <param name="speed">The movement speed</param>
    public void MoveTowards(Vector2 position, float speed)
    {
        rb.linearVelocity = (position - rb.position) * speed;
        /*
        foreach (Rigidbody2D rb in rbs)
        {
            rb.linearVelocity = (position - rb.position) * speed;
        }*/

        //moveRb.position = Vector2.MoveTowards(moveRb.position, position, speed * Time.deltaTime);
    }

    /// <summary>
    /// Rotates the part
    /// </summary>
    /// <param name="rotation">The rotation</param>
    /// <param name="speed">The rotation speed</param>
    public void RotateTowards(Quaternion rotation, float speed)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
    
    /// <summary>
    /// Rotates the part
    /// </summary>
    /// <param name="direction">The up vector</param>
    /// <param name="speed">The rotation speed</param>
    public void RotateTowards(Vector3 direction, float speed)
    {
        //rb.transform.up = Vector3.MoveTowards(rb.transform.up,direction.normalized,speed*Time.deltaTime);
        rb.transform.up = direction;
    }

}
