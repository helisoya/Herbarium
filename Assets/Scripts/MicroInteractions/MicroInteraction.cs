using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a micro interaction
/// </summary>
public abstract class MicroInteraction : MonoBehaviour
{
    public enum InputType
    {
        MousePosition,
        MouseLeftClick,
        MouseRightClick
    }

    public enum EndingType
    {
        SUCCESS,
        FAILURE,
        CANCEL
    }

    [Header("General")]
    [SerializeField] protected float itemSpeed = 5f;
    [SerializeField] protected float rotateSpeed = 15f;
    [SerializeField] protected Camera microInteractionCamera;
    protected ForagingPickable currentObject;
    protected Vector2 mousePosition;
    protected string currentPlantId;
    public bool inMicroInteraction {get; private set;}
    

    /// <summary>
    /// Starts the micro interaction
    /// </summary>
    /// <param name="plantId">The plant linked to this interaction</param>
    protected abstract void OnStart(string plantId);

    /// <summary>
    /// End the interaction
    /// </summary>
    /// <param name="type"The ending type</param>
    protected abstract void OnEnd(EndingType type);

    /// <summary>
    /// On Update Callback
    /// </summary>
    protected abstract void OnUpdate();

    /// <summary>
    /// On tool use callback
    /// </summary>
    protected abstract void OnToolUse();

    /// <summary>
    /// Starts the micro interaction
    /// </summary>
    /// <param name="plantId">The plant linked to this interaction</param>
    public void StartInteraction(string plantId)
    {
        currentPlantId = plantId;
        inMicroInteraction = true;
        OnStart(plantId);
    }

    /// <summary>
    /// End the interaction
    /// </summary>
    /// <param name="type">The ending type</param>
    public void EndInteraction(EndingType type)
    {
        inMicroInteraction = false;
        OnEnd(type);
        Player.instance.StopMicroInteraction(type);
    }

    /// <summary>
    /// Forwards an input to the micro interaction
    /// </summary>
    /// <param name="inputType">The input type</param>
    /// <param name="inputValue">The input value</param>
    public void ForwardInput(InputType type, InputValue inputValue)
    {
        if(!inMicroInteraction) return;

        switch (type)
        {
            case InputType.MousePosition:
                mousePosition = inputValue.Get<Vector2>();
                break;
            case InputType.MouseRightClick:
                if(currentObject && inputValue.isPressed)
                {
                    OnToolUse();
                }
                break;

            case InputType.MouseLeftClick:
                if ((!Settings.instance.IsHoldModeEnabled() && !inputValue.isPressed) || (Settings.instance.IsHoldModeEnabled() && inputValue.isPressed && currentObject) ) 
                {
                    if (currentObject)
                    {
                        currentObject.Drop();
                        currentObject = null;
                    }
                }
                else if(!currentObject && inputValue.isPressed)
                {
                    Vector2 mousePosInWorld = microInteractionCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, microInteractionCamera.nearClipPlane));
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosInWorld, 0.1f);
                    foreach(Collider2D collider in colliders)
                    {
                        if(collider.attachedRigidbody && collider.attachedRigidbody.tag == "Player" && collider.attachedRigidbody.TryGetComponent<ForagingPickablePart>(out ForagingPickablePart obj))
                        {
                            ForagingPickable parent = obj.GetParent();
                            if (parent.CanBePickedUp())
                            {
                                parent.Pickup(obj);
                                currentObject = parent;
                                break;
                            }
                        }
                    }
                }
                break;
        }
    }

    void Update()
    {
        if(!inMicroInteraction) return;

        if (currentObject)
        {
            Vector2 mousePosInWorld = microInteractionCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, microInteractionCamera.nearClipPlane));

            float distance = Vector2.Distance(currentObject.transform.position, mousePosInWorld);
            Vector2 direction = mousePosInWorld - new Vector2(currentObject.transform.position.x, currentObject.transform.position.y);

            if (distance >= 0.1f && currentObject.CanRotate())
            {
                currentObject.RotateTowards(direction.normalized,rotateSpeed);
            }

            currentObject.MoveTowards(mousePosInWorld, itemSpeed);

            return;
        }

        OnUpdate();
    }
}
