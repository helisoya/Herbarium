using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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

    [Header("Hint")]
    [SerializeField] private GameObject hintsRoot;
    [SerializeField] private Button[] hintsButtons;
    [SerializeField] private LocalizedText[] hintsTexts;


    [Header("Audio Events")]
    [SerializeField] private UnityEvent onHoverHintButton;
    [SerializeField] private UnityEvent onHintOpen;
    [SerializeField] private UnityEvent onHoverIndividualHint;
    [SerializeField] private UnityEvent onClickIndividualHint;
    [SerializeField] private UnityEvent onHintClosed;


    public override void GoLeft()
    {
        CloseHints(false);
        gui.InvokeOnLeftEvent();
        
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
        CloseHints(false);
        string[] allPlants = GameManager.instance.GetPlantDatabase().GetExistingPlants();
        gui.InvokeOnRightEvent();

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
        CloseHints(false);
    }

    public override void OnOpen()
    {
        gui.SetMarkers(true,false);
        gui.SetLeftRightActive(true,true);
        RefreshVisuals();
    }

    public void InvokeOnHoverButton()
    {
        onHoverHintButton.Invoke();
    }

    public void InvokeOnHoverHint(int index)
    {
        if(hintsButtons[index].interactable) onHoverIndividualHint.Invoke();
    }

    /// <summary>
    /// Close the hints menu
    /// </summary>
    /// <param name="playSound">True if the closing sound can be played</param>
    public void CloseHints(bool playSound = true)
    {
        if(playSound) onHintClosed.Invoke();
        hintsRoot.SetActive(false);
    }

    /// <summary>
    /// Opens the hints menu
    /// </summary>
    public void OpenHints()
    {
        onHintOpen.Invoke();
        hintsRoot.SetActive(true);

        for(int i = 0; i < hintsButtons.Length; i++)
        {
            hintsButtons[i].interactable = true;
            hintsTexts[i].SetNewKey("Herbarium_Plant_Hint_"+i);
        }
    }

    /// <summary>
    /// Reveal an hint
    /// </summary>
    /// <param name="index">The hint index</param>
    public void RevealHint(int index)
    {
        onClickIndividualHint.Invoke();

        hintsButtons[index].interactable = false;
        hintsTexts[index].SetNewKey(Plant.GetHint(GameManager.instance.GetPlantDatabase().GetExistingPlants()[localPageIndex],index));
    }

    /// <summary>
    /// Refreshs the game's visuals
    /// </summary>
    private void RefreshVisuals()
    {
        onPageChange.Invoke(localPageIndex);

        string plantId = GameManager.instance.GetPlantDatabase().GetExistingPlants()[localPageIndex];
        bool wasPlantFound = GameManager.instance.GetPlayerDataHandler().IsUnlockedInHerbarium(plantId);

        rootLog.SetActive(wasPlantFound);
        textName.SetNoText(false);
        textLatinName.SetNoText(!wasPlantFound);
        textInformations.SetNoText(!wasPlantFound);
        textSpecifics.SetNoText(!wasPlantFound);

        Plant plantData = GameManager.instance.GetPlantDatabase().GetPlant(plantId);
        imagePlant.sprite = plantData.herbariumSprite;

        if (wasPlantFound)
        {
            
            textName.SetNewKey(Plant.GetName(plantId));
            textLatinName.SetNewKey(Plant.GetLatinName(plantId));
            textInformations.SetNewKey(Plant.GetLore(plantId));
            textSpecifics.SetNewKey(Plant.GetSpecifics(plantId));
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
