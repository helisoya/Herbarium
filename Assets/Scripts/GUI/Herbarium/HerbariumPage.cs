using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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

    [Header("Audio Event")]
    [SerializeField] protected UnityEvent<int> onPageChange;

    protected int localPageIndex;

    /// <summary>
	/// Opens the page
	/// </summary>
	/// <param name="localPageIndex">The local page index (N page of plant entries, ...)</param>
    public void Open(int localPageIndex)
    {
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

    /// <summary>
    /// Go backwards in the Herbarium (to page n-1)
    /// </summary>
    public abstract void GoLeft();

    /// <summary>
    /// Go fowards in the Herbarium (to page n+1)
    /// </summary>
    public abstract void GoRight();
}
