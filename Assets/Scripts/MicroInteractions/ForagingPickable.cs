using UnityEngine;

public class ForagingPickable : MonoBehaviour
{
    public enum PickableType
    {
        PLANT,
        CUTTER
    }


    [SerializeField] private Rigidbody2D moveRb;
    [SerializeField] private Rigidbody2D[] rbs;
    [SerializeField] private Joint2D[] joints;
    [SerializeField] private bool canBePickedUp;
    [SerializeField] private PickableType type;
    [SerializeField] private bool canRotate;

    private ForagingPickablePart currentMovablePart;

    void Start()
    {
        if (canBePickedUp) EnablePickup();
    }

    public bool CanRotate()
    {
        return canRotate;
    }

    public bool CanBePickedUp()
    {
        return canBePickedUp;
    }

    public PickableType GetPickableType()
    {
        return type;
    }

    public void EnablePickup()
    {
        foreach (Joint2D joint in joints) Destroy(joint);
        joints = null;

        LayerMask mask = LayerMask.NameToLayer("Default");
        for(int i = 0; i < rbs.Length;i++)
        {
            rbs[i].gravityScale = 1;
            rbs[i].bodyType = RigidbodyType2D.Dynamic;
            rbs[i].excludeLayers = new LayerMask();
            rbs[i].excludeLayers = mask;
            
            for(int j = i+1; j < rbs.Length; j++)
            {
                Physics2D.IgnoreCollision(rbs[i].GetComponent<Collider2D>(),rbs[j].GetComponent<Collider2D>(),true);
            }
        }
        
        canBePickedUp = true;
    }

    public void DisablePickup()
    {
        canBePickedUp = false;
    }


    public void Pickup(ForagingPickablePart part)
    {
        currentMovablePart = part;
        part.Pickup(canRotate);
    }

    public void Drop()
    {
        currentMovablePart.Drop();
        currentMovablePart = null;
    }

    public void MoveTowards(Vector2 position, float speed)
    {
        currentMovablePart.MoveTowards(position, speed);
    }

    public void RotateTowards(Quaternion rotation, float speed)
    {
        currentMovablePart.RotateTowards(rotation, speed);
    }
    
    public void RotateTowards(Vector3 direction, float speed)
    {
        currentMovablePart.RotateTowards(direction, speed);
    }

}
