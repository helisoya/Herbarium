using UnityEngine;

public class ForagingCutPoint : MonoBehaviour
{
    [SerializeField] private ForagingPickable linkedPickable;

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
