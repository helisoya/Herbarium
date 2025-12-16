using System.Linq;
using UnityEngine;
using UnityEngineInternal;

/// <summary>
/// Represents the Plants index in the Herbarium
/// There can be multiple pages of plant index, but only one manager
/// </summary>
public class HerbariumPlantIndex : HerbariumPage
{
    [Header("Plant Index")]
    [SerializeField] protected HerbariumPlantIndexEntry prefabEntry;
    [SerializeField] protected Transform holderLeft;
    [SerializeField] protected Transform holderRight;

    private const int ENTRY_COUNT = 14;


    public override void OnClose()
    {
        foreach (Transform child in holderLeft) Destroy(child.gameObject);
        foreach (Transform child in holderRight) Destroy(child.gameObject);
    }

    public override void OnOpen()
    {
        RefreshVisuals();
    }

    /// <summary>
    /// Refreshes the page's visuals
    /// </summary>
    private void RefreshVisuals()
    {
        // There can be 14 entries per page
        
        string[] allPlants = GameManager.instance.GetPlantDatabase().GetExistingPlants();
        string[] unlockedPages = GameManager.instance.GetPlayerDataHandler().GetHerbariumUnlockedPages();
        bool unlocked;
        int correctedIdx;

        for (int i = 0; i < ENTRY_COUNT && i + ENTRY_COUNT * localPageIndex < allPlants.Length ; i++)
        {
            correctedIdx = i + ENTRY_COUNT * localPageIndex;
            unlocked = false;
            
            for(int j = 0; j < unlockedPages.Length; j++)
            {
                if(allPlants[correctedIdx] == unlockedPages[j])
                {
                    unlocked = true;
                    break;
                }
            }

            Instantiate<HerbariumPlantIndexEntry>(prefabEntry,
            i <= ENTRY_COUNT / 2.0f ? holderLeft : holderRight
            ).Init(i, gui, unlocked ? GameManager.instance.GetPlantDatabase().GetPlant(allPlants[correctedIdx]).Name : "Herbarium_PlantsIndex_Unknown");
        }
    }

    public override void GoLeft()
    {
        if(localPageIndex == 0)
        {
            gui.SetMainPage();
        }
        else
        {
            localPageIndex--;
            RefreshVisuals();
        }
    }

    public override void GoRight()
    {
        string[] allPlants = GameManager.instance.GetPlantDatabase().GetExistingPlants();
        int pagesCount = Mathf.CeilToInt((float)allPlants.Length / ENTRY_COUNT);
    }
}
