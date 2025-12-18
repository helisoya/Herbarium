using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the GUI of the Herbarium
/// </summary>
public class HerbariumGUI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject root;
    [SerializeField] private HerbariumMainPage mainPage;
    [SerializeField] private HerbariumPlantIndex plantIndex;
    [SerializeField] private HerbariumPlant plant;
    [SerializeField] private HerbariumQuestIndex questIndex;
    [SerializeField] private HerbariumQuest quest;


    [Header("Audio Events")]
    [SerializeField] private UnityEvent onOpen;
    [SerializeField] private UnityEvent onClose;
    [SerializeField] private UnityEvent onLeft;
    [SerializeField] private UnityEvent onRight;
    [SerializeField] private UnityEvent onPlantsIndex;
    [SerializeField] private UnityEvent onQuestsIndex;
    [SerializeField] private UnityEvent onHover;


    private HerbariumPage currentPage;

    public bool isOpen { get { return root.activeInHierarchy; } }

    /// <summary>
	/// Opens the herbarium
	/// </summary>
    public void Open()
    {
        if (!isOpen)
        {
            onOpen.Invoke();
            root.SetActive(true);
        }

        SetMainPage();
    }

    /// <summary>
	/// Closes the herbarium
	/// </summary>
    public void Close()
    {
        if (isOpen)
        {
            onClose.Invoke();
            root.SetActive(false);
        }
    }

    /// <summary>
    /// Go left in the Herbarium
    /// </summary>
    public void GoLeft()
    {
        currentPage.GoLeft();
    }

    /// <summary>
    /// Go right in the Herbarium
    /// </summary>
    public void GoRight()
    {
        currentPage.GoRight();
    }

    /// <summary>
    /// Invokes the on right event
    /// </summary>
    public void InvokeOnRightEvent()
    {
        onRight.Invoke();
    }
    
    /// <summary>
    /// Invokes the on left event
    /// </summary>
    public void InvokeOnLeftEvent()
    {
        onLeft.Invoke();
    }

    /// <summary>
    /// Invokes the on hover event
    /// </summary>
    public void InvokeOnHover()
    {
        onHover.Invoke();
    }

    /// <summary>
    /// Opens the plant index
    /// </summary>
    public void QuickOpenPlantIndex()
    {
        onPlantsIndex.Invoke();
        SetPlantIndex(0);
    }

    /// <summary>
    /// Opens the quest index
    /// </summary>
    public void QuickOpenQuestIndex()
    {
        onQuestsIndex.Invoke();
        SetQuestIndex(0);
    }

    /// <summary>
	/// Opens a page in the Herbarium
	/// </summary>
	/// <param name="pageIndex">The page index to open</param>
    public void SetPage(HerbariumPage page, int pageIndex)
    {
        if(currentPage != null) currentPage.Close();
        currentPage = page; 
        page.Open(pageIndex);
    }

    /// <summary>
    /// Opens the main page of the Herbarium
    /// </summary>
    public void SetMainPage()
    {
        SetPage(mainPage,0);
    }

    /// <summary>
    /// Opens the plant index
    /// </summary>
    /// <param name="localIndex">The plant index's local index</param>
    public void SetPlantIndex(int localIndex)
    {
        SetPage(plantIndex,localIndex);
    }

    /// <summary>
    /// Opens the plant description
    /// </summary>
    /// <param name="localIndex">The plant's index</param>
    public void SetPlant(int localIndex)
    {
        SetPage(plant,localIndex);
    }

    /// <summary>
    /// Opens the quest index
    /// </summary>
    /// <param name="localIndex">The quest index's local index</param>
    public void SetQuestIndex(int localIndex)
    {
        SetPage(questIndex,localIndex);
    }

    /// <summary>
    /// Opens the quest description
    /// </summary>
    /// <param name="localIndex">The quest's index</param>
    public void SetQuest(int localIndex)
    {
        SetPage(quest,localIndex);
    }
}
