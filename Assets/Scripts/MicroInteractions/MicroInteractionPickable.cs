using System.Collections.Generic;
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
    [SerializeField] private float individualMassOnDrop = 1.0f;

    [Header("Components")]
    [SerializeField] private Rigidbody2D moveRb;
    [SerializeField] private Rigidbody2D[] rbs;
    [SerializeField] private Joint2D[] joints;

    [Header("Audio")]
    [SerializeField] private UnityEvent onTouchedGround;
    [SerializeField] private UnityEvent onStartTouchPlant;
    [SerializeField] private UnityEvent onStopTouchPlant;
    private bool shouldInvokeOnTouchGround = false;
    private bool shouldInvokeOnStartPlant = false;
    private bool shouldInvokeOnEndPlant = false;

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
        shouldInvokeOnTouchGround = true;
    }

    /// <summary>
    /// Invokes the on start touched plant event
    /// </summary>
    public void InvokeOnStartTouchPlant()
    {
        shouldInvokeOnStartPlant = true;
    }

    /// <summary>
    /// Invokes the on end touched plant event
    /// </summary>
    public void InvokeOnEndTouchPlant()
    {
        shouldInvokeOnEndPlant = true;
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
            rbs[i].constraints = RigidbodyConstraints2D.None;
            rbs[i].mass = individualMassOnDrop;
            
            for(int j = i+1; j < rbs.Length; j++)
            {
                Physics2D.IgnoreCollision(rbs[i].GetComponent<Collider2D>(),rbs[j].GetComponent<Collider2D>(),true);
            }
        }

        PropagateTag("Player",transform);
        
        canBePickedUp = true;
    }

    /// <summary>
    /// Propagates a tag to the object's children
    /// </summary>
    /// <param name="tag">The tag</param>
    /// <param name="obj">The current object</param>
    private void PropagateTag(string tag, Transform obj)
    {

        Stack<Transform> stack = new Stack<Transform>();
        Transform current;
        stack.Push(obj.GetChild(0));
        
        while(stack.Count != 0)
        {
            current = stack.Pop();
            current.tag = tag;
            foreach(Transform child in current)
            {
                stack.Push(child);
            }
        }
    }

    void Update()
    {
        if (shouldInvokeOnTouchGround)
        {
            shouldInvokeOnTouchGround = false;
            onTouchedGround.Invoke();
        }

        if (shouldInvokeOnStartPlant)
        {
            shouldInvokeOnStartPlant = false;
            onStartTouchPlant.Invoke();
        }

        if (shouldInvokeOnEndPlant)
        {
            shouldInvokeOnEndPlant = false;
            onStopTouchPlant.Invoke();
        }
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
