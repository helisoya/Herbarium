using UnityEngine;

/// <summary>
/// Handles the GUI of the Herbarium
/// </summary>
public class HerbariumGUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private HerbariumPlantIndex plantIndex;

    public bool isOpen { get { return root.activeInHierarchy; } }

    /// <summary>
	/// Opens the herbarium
	/// </summary>
	/// <param name="pageIndex">The page index to open</param>
    public void Open(int pageIndex = 0)
    {
        if (!isOpen)
        {
            root.SetActive(true);
        }

        SetPage(pageIndex);
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
	/// Opens a page in the Herbarium
	/// </summary>
	/// <param name="pageIndex">The page index to open</param>
    public void SetPage(int pageIndex)
    {
        plantIndex.Open(0, 0);
    }
}
