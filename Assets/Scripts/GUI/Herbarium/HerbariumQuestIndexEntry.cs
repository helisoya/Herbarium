using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HerbariumQuestIndexEntry : MonoBehaviour
{
    [SerializeField] private LocalizedText label;
    [SerializeField] private Button button;
    [SerializeField] private Image imagePinned;

    private int linkedPage;
    private HerbariumGUI gui;

    /// <summary>
	/// Initialize the component
	/// </summary>
	/// <param name="page">The linked page</param>
	/// <param name="gui">The Herbarium's GUI</param>
    /// <param name="questName">The quest name</param>
    /// <param name="colorBlock">The button's color block</param>
    public void Init(int page, HerbariumGUI gui, string questName, ColorBlock colorBlock)
    {
        this.linkedPage = page;
        this.gui = gui;
        label.SetNewKey(questName);
        button.colors = colorBlock;

        imagePinned.color = GameManager.instance.GetPlayerDataHandler().IsPinned(GameManager.instance.GetPlayerDataHandler().GetKnownQuests()[linkedPage].id) ? Color.white : Color.black;
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
        gui.InvokeOnClickQuestLink();
        gui.SetQuest(linkedPage);
    }

    /// <summary>
    /// Right Click event
    /// </summary>
    public void SwitchPin()
    {
        GameManager.instance.GetPlayerDataHandler().SwitchQuestPin(GameManager.instance.GetPlayerDataHandler().GetKnownQuests()[linkedPage].id);

        bool isPinned = GameManager.instance.GetPlayerDataHandler().IsPinned(GameManager.instance.GetPlayerDataHandler().GetKnownQuests()[linkedPage].id);
        imagePinned.color = isPinned ? Color.white : Color.black;

        gui.InvokeOnPinQuest(isPinned);
    }

    /// <summary>
    /// On Hover event
    /// </summary>
    public void Hover()
    {
        gui.InvokeOnHoverLink();
    }
}
