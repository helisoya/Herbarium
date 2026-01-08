using UnityEngine;

/// <summary>
/// Represents the Backpack's trigger zone in the foraging micro interaction
/// </summary>
public class ForagingBackpack : MonoBehaviour
{
    [SerializeField] private ForagingMicroInteraction microInteraction;
    private bool alreadyActivated;
    private bool activeNextFrame;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.attachedRigidbody.TryGetComponent<MicroInteractionPickablePart>(out MicroInteractionPickablePart pickable) && pickable.GetParent().GetPickableType() == MicroInteractionPickable.PickableType.PLANT)
        {
            pickable.GetParent().DisablePickup();
            activeNextFrame = true;
        }
        
    }

    void Update()
    {
        if(!alreadyActivated && activeNextFrame)
        {
            alreadyActivated = true;
            microInteraction.RaiseFlagPlantInBackpack();
        }
    }
}
