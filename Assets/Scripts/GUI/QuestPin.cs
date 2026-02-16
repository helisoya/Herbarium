using UnityEngine;

/// <summary>
/// Represents a quest pin
/// </summary>
public class QuestPin : MonoBehaviour
{
    [SerializeField] private LocalizedText text;
    private Quest linkedQuest; 

    /// <summary>
    /// Initialize the pin
    /// </summary>
    /// <param name="quest">The quest</param>
    public void Init(Quest quest)
    {
        linkedQuest = quest;
        Refresh();
    }

    /// <summary>
    /// Gets the pin's linked quest's id
    /// </summary>
    /// <returns>Its ID</returns>
    public string GetLinkedID()
    {
        return linkedQuest.id;
    }

    /// <summary>
    /// Refreshs the pin's content
    /// </summary>
    public void Refresh()
    {
        text.SetNewKey(linkedQuest.GetState(GameManager.instance.GetPlayerDataHandler().GetVariable(linkedQuest.linkedVariable)));
    }

    /// <summary>
    /// On click event
    /// </summary>
    public void Click()
    {
        GameGUI.instance.OpenHerbarium();
        GameGUI.instance.HerbariumShowQuestPage(GameManager.instance.GetPlayerDataHandler().KnownQuestIDToIndex(linkedQuest.id));
    }
}
