using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a tab in the options 
/// </summary>
public abstract class OptionsTab : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected RectTransform root;
    

    /// <summary>
    /// Opens the tab
    /// </summary>
    /// <param name="scrollView">The scrollview to apply the tab to</param>
    public void Open(ScrollRect scrollView)
    {
        scrollView.content = root;
        root.gameObject.SetActive(true);
        OnOpen();
    }

    /// <summary>
    /// Closes the tab
    /// </summary>
    public void Close()
    {
        root.gameObject.SetActive(false);
        OnClose();
    }

    /// <summary>
    /// Callback when opened
    /// </summary>
    protected abstract void OnOpen();

    /// <summary>
    /// Callback when closed
    /// </summary>
    protected abstract void OnClose();
}
