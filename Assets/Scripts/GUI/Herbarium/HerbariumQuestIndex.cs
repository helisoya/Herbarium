using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the Quest index in the Herbarium
/// You can pin quests on this screen
/// </summary>
public class HerbariumQuestIndex : HerbariumPage
{
    [Header("Quest Index")]
    [SerializeField] protected GameObject NoQuestsObj; 
    [SerializeField] protected HerbariumQuestIndexEntry prefabEntry;
    [SerializeField] protected Transform holderLeft;
    [SerializeField] protected Transform holderRight;

    [Header("Quest Normal Color")]
    [SerializeField] protected ColorBlock colorNormal;

    [Header("Quest Done Color")]
    [SerializeField] protected ColorBlock colorDone;


    public const int ENTRY_COUNT = 14;

    protected Quest[] knownQuests;

    public override void GoLeft()
    {
        if(localPageIndex == 0)
        {
            gui.SetPlant(GameManager.instance.GetPlantDatabase().GetExistingPlants().Length-1);
        }
        else
        {
            localPageIndex--;
            RefreshVisuals();
        }
    }

    public override void GoRight()
    {
        
        int pagesCount = Mathf.CeilToInt((float)knownQuests.Length / ENTRY_COUNT);

        if(knownQuests.Length == 0 || localPageIndex == pagesCount - 1)
        {
            gui.SetQuest(0);
        }
        else
        {
            localPageIndex++;
            RefreshVisuals();
        }
    }

    public override void OnClose()
    {
        foreach (Transform child in holderLeft) Destroy(child.gameObject);
        foreach (Transform child in holderRight) Destroy(child.gameObject);

        knownQuests = null;
    }

    public override void OnOpen()
    {
        knownQuests = GameManager.instance.GetPlayerDataHandler().GetKnownQuests();
        RefreshVisuals();
    }

    /// <summary>
    /// Refreshs the visuals of the herbarium
    /// </summary>
    private void RefreshVisuals()
    {
        // There can be 14 entries per page

        foreach (Transform child in holderLeft) Destroy(child.gameObject);
        foreach (Transform child in holderRight) Destroy(child.gameObject);

        if(knownQuests.Length == 0)
        {
            NoQuestsObj.SetActive(true);
        }
        else
        {
            NoQuestsObj.SetActive(false);
        
            string[] allPlants = GameManager.instance.GetPlantDatabase().GetExistingPlants();
            string[] unlockedPages = GameManager.instance.GetPlayerDataHandler().GetHerbariumUnlockedPages();
            bool finished;
            int correctedIdx;

            for (int i = 0; i < ENTRY_COUNT && i + ENTRY_COUNT * localPageIndex < knownQuests.Length ; i++)
            {
                correctedIdx = i + ENTRY_COUNT * localPageIndex;
                finished = GameManager.instance.GetPlayerDataHandler().GetVariable(knownQuests[correctedIdx].linkedVariable) == 100;

                Instantiate<HerbariumQuestIndexEntry>(prefabEntry,
                i <= ENTRY_COUNT / 2.0f ? holderLeft : holderRight
                ).Init(correctedIdx, gui, knownQuests[correctedIdx].Name, finished ? colorDone : colorNormal);
            }
        }
    }
}
