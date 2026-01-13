using UnityEngine;

/// <summary>
/// Represents a cut point in the foraging micro interaction
/// </summary>
public class ForagingCutPoint : MonoBehaviour
{
    [SerializeField] private MicroInteractionPickable linkedPickable;

    /// <summary>
    /// Cuts the point and release the linked pickable
    /// </summary>
    public void Cut()
    {
        if (linkedPickable)
        {
            linkedPickable.transform.SetParent(null);
            linkedPickable.EnablePickup();
            Destroy(gameObject);
        }
    }
}
