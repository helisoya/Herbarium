using UnityEngine;

/// <summary>
/// Handles the GUI of the Herbarium
/// </summary>
public class HerbariumGUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private HerbariumMainPage mainPage;
    [SerializeField] private HerbariumPlantIndex plantIndex;
    [SerializeField] private HerbariumPlant plant;
    [SerializeField] private HerbariumQuestIndex questIndex;
    [SerializeField] private HerbariumQuest quest;

    private HerbariumPage currentPage;

    public bool isOpen { get { return root.activeInHierarchy; } }

    /// <summary>
	/// Opens the herbarium
	/// </summary>
    public void Open()
    {
        if (!isOpen)
        {
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
