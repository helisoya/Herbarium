using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the Quest description page in the herbarium
/// </summary>
public class HerbariumQuest : HerbariumPage
{
    [Header("Quest")]
    [SerializeField] private LocalizedText textName;
    [SerializeField] private LocalizedText textDescription;
    [SerializeField] private LocalizedText textLore;
    [SerializeField] private LocalizedText textProgress;
    [SerializeField] private Image imagePin;
    [SerializeField] private Image imageQuest;


    protected Quest currentQuest;

    public override void GoLeft()
    {
        gui.InvokeOnLeftEvent();

        if(localPageIndex == 0)
        {
            Quest[] allQuests = GameManager.instance.GetPlayerDataHandler().GetKnownQuests();
            int pagesCount = Mathf.CeilToInt((float)allQuests.Length / HerbariumQuestIndex.ENTRY_COUNT);

            gui.SetQuestIndex(pagesCount-1);
        }
        else
        {
            localPageIndex--;
            RefreshVisuals();
        }
    }

    public override void GoRight()
    {
        Quest[] allQuests = GameManager.instance.GetPlayerDataHandler().GetKnownQuests();

        if(localPageIndex == allQuests.Length - 1)
        {
            // Nothing afterwards
        }
        else
        {
            gui.InvokeOnRightEvent();
            localPageIndex++;
            RefreshVisuals();
        }
    }

    public override void OnClose()
    {
    }

    public override void OnOpen()
    {
        gui.SetMarkers(false,true);
        RefreshVisuals();
    }

    /// <summary>
    /// Switch the quest pin
    /// </summary>
    public void SwitchQuestPin()
    {
        GameManager.instance.GetPlayerDataHandler().SwitchQuestPin(currentQuest.id);
        imagePin.color = GameManager.instance.GetPlayerDataHandler().IsPinned(currentQuest.id) ? Color.white : Color.black;
    }


    /// <summary>
    /// Refreshs the game's visuals
    /// </summary>
    private void RefreshVisuals()
    {
        onPageChange.Invoke(localPageIndex);

        Quest[] quests = GameManager.instance.GetPlayerDataHandler().GetKnownQuests();

        gui.SetLeftRightActive(true,localPageIndex < quests.Length - 1);
        
        currentQuest = quests[localPageIndex];

        imagePin.color = GameManager.instance.GetPlayerDataHandler().IsPinned(currentQuest.id) ? Color.white : Color.black;

        textName.SetNewKey(currentQuest.Name);
        textDescription.SetNewKey(currentQuest.Description);
        textLore.SetNewKey(currentQuest.Lore);
        textProgress.SetNewKey(currentQuest.GetState(GameManager.instance.GetPlayerDataHandler().GetVariable(currentQuest.linkedVariable)));
        imageQuest.sprite = currentQuest.questSprite;
    }
}
