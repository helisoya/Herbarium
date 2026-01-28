using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
        MouseRightClick,
        Pause
    }

    public enum EndingType
    {
        SUCCESS,
        FAILURE,
        CANCEL
    }

    public struct PickupAudioData
    {
        public MicroInteractionPickable.PickableType type;
        public GameObject movingObject;
    }

    [Header("General")]
    [SerializeField] protected float itemSpeed = 5f;
    [SerializeField] protected float rotateSpeed = 15f;
    [SerializeField] protected Camera microInteractionCamera;
    [SerializeField] protected PauseMenu pauseMenu;
    [SerializeField] protected Fade fade;

    [Header("Tutorial")]
    [SerializeField] protected GameObject grabTutorial;
    [SerializeField] protected GameObject cutTutorial;

    protected MicroInteractionPickable currentObject;
    protected Vector2 mousePosition;
    protected string currentPlantId;
    public bool inMicroInteraction {get; private set;}

    [Header("General Audio")]
    [SerializeField] private UnityEvent onStartMicroInteraction;
    [SerializeField] private UnityEvent<EndingType> onEndMicroInteraction;
    [SerializeField] private UnityEvent<PickupAudioData> onPickUpObject;
    [SerializeField] private UnityEvent<PickupAudioData> onDropObject;
    [SerializeField] private UnityEvent<float> onMoveObject;
    

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
        onStartMicroInteraction.Invoke();

        grabTutorial.SetActive(true);
        cutTutorial.SetActive(false);

        fade.ForceAlphaTo(1);
        fade.FadeTo(0);

        GameManager.instance.GetCursor().ChangeCursor(HerbariumCursor.CursorType.FORAGENORMAL);

        OnStart(plantId);
    }

    /// <summary>
    /// End the interaction
    /// </summary>
    /// <param name="type">The ending type</param>
    public void EndInteraction(EndingType type)
    {
        inMicroInteraction = false;
        onEndMicroInteraction.Invoke(type);

        if (currentObject)
        {
            currentObject.Drop();
            currentObject = null;
        }

        OnEnd(type);
        StartCoroutine(RoutineEnd(type));
    }

    /// <summary>
    /// Routine for the end of the game
    /// </summary>
    /// <param name="type">The ending type</param>
    private IEnumerator RoutineEnd(EndingType type)
    {
        fade.FadeTo(1);
        yield return new WaitForEndOfFrame();
        while (fade.fading)
        {
            yield return new WaitForEndOfFrame();
        }
        GameManager.instance.GetCursor().ChangeCursor(HerbariumCursor.CursorType.NORMAL);
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
                if(!pauseMenu.isOpen && currentObject && inputValue.isPressed)
                {
                    OnToolUse();
                }
                break;

            case InputType.MouseLeftClick:
                if(pauseMenu.isOpen) break;
                if ((!Settings.instance.IsToggleGrabEnabled() && !inputValue.isPressed) || (Settings.instance.IsToggleGrabEnabled() && inputValue.isPressed && currentObject) ) 
                {
                    if (currentObject)
                    {
                        GameManager.instance.GetCursor().ChangeCursor(HerbariumCursor.CursorType.FORAGENORMAL);
                        cutTutorial.SetActive(false);
                        onDropObject.Invoke(new PickupAudioData() {movingObject = currentObject.GetCurrentMovingPart().gameObject, type = currentObject.GetPickableType()});
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
                        if(collider.attachedRigidbody && collider.attachedRigidbody.TryGetComponent<MicroInteractionPickablePart>(out MicroInteractionPickablePart obj))
                        {
                            MicroInteractionPickable parent = obj.GetParent();
                            if (parent.CanBePickedUp())
                            {
                                GameManager.instance.GetCursor().ChangeCursor(HerbariumCursor.CursorType.FORAGEHOLD);
                                cutTutorial.SetActive(parent.GetPickableType() == MicroInteractionPickable.PickableType.CUTTER);
                                parent.Pickup(obj);
                                onPickUpObject.Invoke(new PickupAudioData() {movingObject = parent.GetCurrentMovingPart().gameObject, type = parent.GetPickableType()});
                                currentObject = parent;
                                break;
                            }
                        }
                    }
                }
                break;
            case InputType.Pause:
                if(pauseMenu.isOpen) pauseMenu.Close();
                else pauseMenu.Open();
                break;
        }
    }

    void Update()
    {
        if(!inMicroInteraction || pauseMenu.isOpen) return;

        if (currentObject)
        {
            MicroInteractionPickablePart part = currentObject.GetCurrentMovingPart();
            Vector2 mousePosInWorld = microInteractionCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, microInteractionCamera.nearClipPlane));

            float distance = Vector2.Distance(part.transform.position, mousePosInWorld);
            Vector2 direction = mousePosInWorld - new Vector2(part.transform.position.x, part.transform.position.y);

            if (distance >= 0.1f && currentObject.CanRotate())
            {
                currentObject.RotateTowards(direction.normalized,rotateSpeed);
            }

            currentObject.MoveTowards(mousePosInWorld, itemSpeed);
            onMoveObject.Invoke(distance);
        }

        OnUpdate();
    }
}
