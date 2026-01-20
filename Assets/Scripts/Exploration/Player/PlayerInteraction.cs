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
    private Vector2 mousePosition;
    private InteractableObject currentObject;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerBody.position,interactionDistance);
    }

    void Update()
    {
        InteractableObject selected = null;
        float distTemp;

        // Check at mouse
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        if(Physics.Raycast(ray, out RaycastHit hitInfo,100f,interactionMask))
        {
            distTemp = Vector3.Distance(hitInfo.collider.bounds.center, playerBody.position);

            if(distTemp < interactionDistance)
            {
                selected = hitInfo.collider.GetComponent<InteractableObject>();
            }
        }

        /*
        // Check by distance
        if (!selected)
        {
            Collider[] colliders = Physics.OverlapSphere(playerBody.position,interactionDistance,interactionMask);
            float minDistance = 999f;
            
            foreach(Collider collider in colliders)
            {
                distTemp = Vector3.Distance(collider.bounds.center, playerBody.position);
                if(distTemp < minDistance)
                {
                    minDistance = distTemp;
                    selected = collider.GetComponent<InteractableObject>();
                }
            }
        }
        */

        if(selected != currentObject)
        {
            if(currentObject) currentObject.SetActive(false);
            currentObject = selected;
        }

        // Update if the interactionIcon should be shown
        if (currentObject)
        {
            float distance = Vector3.Distance(playerBody.position,currentObject.transform.position);
            currentObject.SetActive(distance <= interactionDistance);
        }
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
    public void TryInterract()
    {

        if (currentObject != null)
        {
            float distance = Vector3.Distance(playerBody.position,currentObject.transform.position);
            if(distance > interactionDistance) return;

            if (currentObject.stopPlayerOnInterract)
            {
                Player.instance.StopPlayerMovements();
            }


            currentObject.SetActive(false); 
            
            currentObject.Interract();

            currentObject = null;
        }
    }
}
