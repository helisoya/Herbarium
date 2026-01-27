using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the interactions for the player
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerController controller;
    private Vector2 mousePosition;
    private InteractableObject currentObject;
    private InteractableObject willInteractWith;
    private bool closingIn;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerBody.position,interactionDistance);
    }

    void Update()
    {
        if(closingIn)
        {
            if (Vector3.Distance(playerBody.position, willInteractWith.transform.position) <= interactionDistance)
            {
                Interact();
                willInteractWith = null;
                closingIn = false;
            }
        }

        InteractableObject selected = null;

        // Check at mouse
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        if(Physics.Raycast(ray, out RaycastHit hitInfo,100f,interactionMask))
        {
            selected = hitInfo.collider.GetComponent<InteractableObject>();
        }

        if(selected != currentObject)
        {
            if(currentObject) currentObject.SetActive(false);
            currentObject = selected;
            if(currentObject)currentObject.SetActive(true);
            closingIn = false;
        }
    }

    /// <summary>
    /// Disables the closing in tag
    /// </summary>
    public void DisableClosingInTag()
    {
        closingIn = false;
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
    /// Starts an interaction with the currently selected interractable object
    /// </summary>
    /// <returns>True if an interaction hapenned</returns>
    public bool TryInterract()
    {
        if (currentObject != null)
        {
            float distance = Vector3.Distance(playerBody.position,currentObject.transform.position);
            willInteractWith = currentObject;
            
            if(distance > interactionDistance){
                closingIn = true;
                controller.SetTargetPosition(currentObject.transform.position);
            }
            else
            {
                closingIn = false;
                Interact();    
            }

            return true;
        }
        return false;
    }

    /// <summary>
    /// Starts the current interaction
    /// </summary>
    private void Interact()
    {
        if(!willInteractWith) return;

        if (willInteractWith.stopPlayerOnInterract)
        {
            Player.instance.StopPlayerMovements();
        }

        willInteractWith.SetActive(false); 
        string trigger = willInteractWith.GetAnimationTrigger();
        if(!string.IsNullOrEmpty(trigger)) playerAnimator.SetTrigger(trigger); //isBending for plants, isSpeaking for NPCs, isAction for everything else
        willInteractWith.Interract();

        willInteractWith = null;
        currentObject = null;
    }
}
