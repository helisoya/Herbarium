using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the page in the Herbarium that describes a specific plant
/// </summary>
public class HerbariumPlant : HerbariumPage
{
    [Header("Plant")]
    [SerializeField] private LocalizedText textName;
    [SerializeField] private LocalizedText textLatinName;
    [SerializeField] private LocalizedText textCategory;
    [SerializeField] private LocalizedText textInformations;
    [SerializeField] private LocalizedText textSpecifics;
    [SerializeField] private LocalizedText textLog;
    [SerializeField] private GameObject rootLog;
    [SerializeField] private Image imagePlant;

    public override void GoLeft()
    {
        if(localPageIndex == 0)
        {
            string[] allPlants = GameManager.instance.GetPlantDatabase().GetExistingPlants();
            int pagesCount = Mathf.CeilToInt((float)allPlants.Length / HerbariumPlantIndex.ENTRY_COUNT);

            gui.SetPlantIndex(pagesCount-1);
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

        if(localPageIndex == allPlants.Length - 1)
        {
            gui.SetQuestIndex(0);
        }
        else
        {
            localPageIndex++;
            RefreshVisuals();
        }
    }

    public override void OnClose()
    {
    }

    public override void OnOpen()
    {
        RefreshVisuals();
    }


    /// <summary>
    /// Refreshs the game's visuals
    /// </summary>
    private void RefreshVisuals()
    {
        string plantId = GameManager.instance.GetPlantDatabase().GetExistingPlants()[localPageIndex];
        string[] plantsFound = GameManager.instance.GetPlayerDataHandler().GetHerbariumUnlockedPages();
        bool wasPlantFound = false;


        foreach(string plant in plantsFound)
        {
            if (plant.Equals(plantId))
            {
                wasPlantFound = true;
                break;
            }
        }

        rootLog.SetActive(wasPlantFound);
        textName.SetNoText(false);
        textLatinName.SetNoText(!wasPlantFound);
        textInformations.SetNoText(!wasPlantFound);
        textSpecifics.SetNoText(!wasPlantFound);

        Plant plantData = GameManager.instance.GetPlantDatabase().GetPlant(plantId);
        imagePlant.sprite = plantData.herbariumSprite;

        if (wasPlantFound)
        {
            
            textName.SetNewKey(plantData.Name);
            textLatinName.SetNewKey(plantData.LatinName);
            textInformations.SetNewKey(plantData.Lore);
            textSpecifics.SetNewKey(plantData.Specifics);
            textCategory.SetNewKey(plantData.Category);
            imagePlant.color = Color.white;
            

            textLog.SetInjectors(new string[]{"Swamp","25/12"},false);
            textLog.SetNewKey("Herbarium_Plant_Log");
        }
        else
        {
            textName.SetNewKey("Herbarium_PlantsIndex_Unknown");
            textCategory.SetNewKey("Herbarium_PlantsIndex_Unknown");
            imagePlant.color = Color.black;
        }

    }
}
