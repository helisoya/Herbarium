using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents a micro interaction pickable
/// A pickable is composed of multiple parts
/// </summary>
public class MicroInteractionPickable : MonoBehaviour
{
    public enum PickableType
    {
        PLANT,
        CUTTER
    }

    [Header("Infos")]
    [SerializeField] private bool canBePickedUp;
    [SerializeField] private PickableType type;
    [SerializeField] private bool canRotate;

    [Header("Components")]
    [SerializeField] private Rigidbody2D moveRb;
    [SerializeField] private Rigidbody2D[] rbs;
    [SerializeField] private Joint2D[] joints;

    [Header("Audio")]
    [SerializeField] private UnityEvent onTouchedGround;
    [SerializeField] private UnityEvent onStartTouchPlant;
    [SerializeField] private UnityEvent onStopTouchPlant;

    private MicroInteractionPickablePart currentMovablePart;

    void Start()
    {
        if (canBePickedUp) EnablePickup();
    }

    /// <summary>
    /// Gets if the pickable can be rotated
    /// </summary>
    /// <returns>True if it can be rotated</returns>
    public bool CanRotate()
    {
        return canRotate;
    }

    /// <summary>
    /// Gets if the pickable can be picked up
    /// </summary>
    /// <returns>True if it can be picked up</returns>
    public bool CanBePickedUp()
    {
        return canBePickedUp;
    }

    /// <summary>
    /// Gets the pickable type
    /// </summary>
    /// <returns>The pickable type</returns>
    public PickableType GetPickableType()
    {
        return type;
    }

    /// <summary>
    /// Gets the current moving part of the pickable
    /// </summary>
    /// <returns>The current moving part</returns>
    public MicroInteractionPickablePart GetCurrentMovingPart()
    {
        return currentMovablePart;
    }

    /// <summary>
    /// Invokes the on touched ground event
    /// </summary>
    public void InvokeOnTouchGround()
    {
        onTouchedGround.Invoke();
    }

    /// <summary>
    /// Enables the pickup for this pickable
    /// </summary>
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

    /// <summary>
    /// Disables the pickup for this pickable
    /// </summary>
    public void DisablePickup()
    {
        canBePickedUp = false;
    }

    /// <summary>
    /// Pickup the object
    /// </summary>
    /// <param name="part">The part that is used for movements</param>
    public void Pickup(MicroInteractionPickablePart part)
    {
        currentMovablePart = part;
        part.Pickup(canRotate);
    }

    /// <summary>
    /// Drops the object
    /// </summary>
    public void Drop()
    {
        currentMovablePart.Drop();
        currentMovablePart = null;
    }

    /// <summary>
    /// Moves the object towards a position
    /// </summary>
    /// <param name="position">The position</param>
    /// <param name="speed">The speed</param>
    public void MoveTowards(Vector2 position, float speed)
    {
        currentMovablePart.MoveTowards(position, speed);
    }

    /// <summary>
    /// Rotates the object
    /// </summary>
    /// <param name="rotation">The rotation</param>
    /// <param name="speed">The speed</param>
    public void RotateTowards(Quaternion rotation, float speed)
    {
        currentMovablePart.RotateTowards(rotation, speed);
    }
    
    /// <summary>
    /// Rotates the object
    /// </summary>
    /// <param name="direction">The up direction</param>
    /// <param name="speed">The speed</param>
    public void RotateTowards(Vector3 direction, float speed)
    {
        currentMovablePart.RotateTowards(direction, speed);
    }

}
