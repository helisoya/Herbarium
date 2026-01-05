using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ForagingPickablePart : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private ForagingPickable parent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public ForagingPickable GetParent()
    {
        return parent;
    }

    public void Pickup(bool canRotate)
    {
        //SetRigidbodyGravityScale(0f);
        //SetRigidbodyType(RigidbodyType2D.Kinematic);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.freezeRotation = !canRotate;
    }

    public void Drop()
    {
        //SetRigidbodyGravityScale(1f);
        //SetRigidbodyType(RigidbodyType2D.Dynamic);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        rb.freezeRotation = false;
    }

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


    public void RotateTowards(Quaternion rotation, float speed)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
    
    public void RotateTowards(Vector3 direction, float speed)
    {
        rb.transform.up = direction.normalized;
    }

}
