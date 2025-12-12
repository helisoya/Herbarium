using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a "page" in the Herbarium
/// A page object can represent multiple actual pages
/// Ex : One HerbariumPage for every plant description pages
/// </summary>
public abstract class HerbariumPage : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] protected GameObject root;
    [SerializeField] protected HerbariumGUI gui;

    protected int pageIndex;
    protected int localPageIndex;

    /// <summary>
	/// Opens the page
	/// </summary>
	/// <param name="pageIndex">The total page index</param>
	/// <param name="localPageIndex">The local page index (N page of plant entries, ...)</param>
    public void Open(int pageIndex, int localPageIndex)
    {
        this.pageIndex = pageIndex;
        this.localPageIndex = localPageIndex;
        root.SetActive(true);
        OnOpen();
    }

    /// <summary>
	/// Closes the page
	/// </summary>
    public void Close()
    {
        root.SetActive(false);
        OnClose();
    }

    /// <summary>
	/// Called when opening the page
	/// </summary>
    public abstract void OnOpen();

    /// <summary>
	/// Called when closing the page
	/// </summary>
    public abstract void OnClose();
}
