using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a button that can open a specific page on the plant index 
/// </summary>
public class HerbariumPlantIndexEntry : MonoBehaviour
{
    [SerializeField] private LocalizedText label;
    [SerializeField] private Button button;

    private int linkedPage;
    private HerbariumGUI gui;

    /// <summary>
	/// Initialize the component
	/// </summary>
	/// <param name="page">The linked page</param>
	/// <param name="gui">The Herbarium's GUI</param>
    /// <param name="plantId">The plant's ID</param>
    /// <param name="colorBlock">The button's color block</param>
    public void Init(int page, HerbariumGUI gui, string plantId, ColorBlock colorBlock)
    {
        this.linkedPage = page;
        this.gui = gui;
        label.SetNewKey(plantId);
        button.colors = colorBlock;
    }

    /// <summary>
	/// Changes the currently linked page
	/// </summary>
	/// <param name="page">The herbarium page</param>
    public void SetLinkedPage(int page)
    {
        this.linkedPage = page;
    }

    /// <summary>
	/// On Click event
	/// </summary>
    public void Click()
    {
        gui.SetPlant(linkedPage);
    }
}
