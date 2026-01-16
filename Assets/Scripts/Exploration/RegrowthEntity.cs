using UnityEngine;

/// <summary>
/// Represents an entity in the regrowth system
/// </summary>
public class RegrowthEntity : MonoBehaviour
{
    [SerializeField] private string plantId;
    [SerializeField] private string entityId;

    void Start()
    {
        Map.instance.RegisterRegrowthEntity(this);
        RefreshEntity();
    }

    void OnDestroy()
    {
        Map.instance.UnRegisterRegrowthEntity(this);
    }

    /// <summary>
    /// Refreshs the entity
    /// </summary>
    public void RefreshEntity()
    {
        gameObject.SetActive(GameManager.instance.GetPlayerDataHandler().HasEntityRegrown(entityId));
    }

    /// <summary>
    /// Tags the entity as not regrown (will need to grow for X days before appearing again)
    /// </summary>
    public void TagEntityAsNotRegrown()
    {
       gameObject.SetActive(false);
       GameManager.instance.GetPlayerDataHandler().RegisterEntityHasNotRegrown(plantId,entityId); 
    }
}
