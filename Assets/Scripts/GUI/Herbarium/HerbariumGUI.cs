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
    [SerializeField] private GameObject buttonLeft;
    [SerializeField] private GameObject buttonRight;
    [SerializeField] private RectTransform markerPlant;
    [SerializeField] private RectTransform markerQuest;
    [SerializeField] private RectTransform markerMiddle;


    [Header("Audio Events")]
    [SerializeField] private UnityEvent onOpen;
    [SerializeField] private UnityEvent onClose;
    [SerializeField] private UnityEvent onLeft;
    [SerializeField] private UnityEvent onRight;
    [SerializeField] private UnityEvent onPlantsIndex;
    [SerializeField] private UnityEvent onQuestsIndex;
    [SerializeField] private UnityEvent onHoverLink;
    [SerializeField] private UnityEvent onHoverChangePage;
    [SerializeField] private UnityEvent<bool> onPinQuest;


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
    /// Sets the positions of the markers
    /// </summary>
    /// <param name="markerPlantFront">True if the plant marker is up front</param>
    /// <param name="markerQuestFront">True if the quest marker is up front</param>
    public void SetMarkers(bool markerPlantFront, bool markerQuestFront)
    {
        markerMiddle.SetAsFirstSibling();
        if(!markerPlantFront) markerPlant.SetAsFirstSibling();
        if(!markerQuestFront) markerQuest.SetAsFirstSibling();
    }

    /// <summary>
    /// Sets if the left & right buttons are active
    /// </summary>
    /// <param name="leftActive">True if the left button is active</param>
    /// <param name="rightActive">True if the right button is active</param>
    public void SetLeftRightActive(bool leftActive, bool rightActive)
    {
        buttonLeft.SetActive(leftActive);
        buttonRight.SetActive(rightActive);
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
    /// Invokes the on hover change page event
    /// </summary>
    public void InvokeOnHoverChangePage()
    {
        onHoverChangePage.Invoke();
    }

    /// <summary>
    /// Invokes the on hover link event
    /// </summary>
    public void InvokeOnHoverLink()
    {
        onHoverLink.Invoke();
    }

    /// <summary>
    /// Invokes the on pin Quest Event
    /// </summary>
    /// <param name="isPinned">True if the quest is pinned</param>
    public void InvokeOnPinQuest(bool isPinned)
    {
        onPinQuest.Invoke(isPinned);
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
