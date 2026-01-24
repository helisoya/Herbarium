using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Represents the general option menu
/// Actual settings are handled in individual tabs
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject root;
    [SerializeField] private ScrollRect tabsRoot;
    [SerializeField] private OptionsTab[] tabs;
    [SerializeField] private ColorPicker colorPicker;
    [SerializeField] private RectTransform tipRoot;
    [SerializeField] private LocalizedText tipText;
    private OptionsTab currentTab = null;
    private int currentTabIdx = 0;
    
    public bool isOpen{get{return root.activeInHierarchy;}}

    /// <summary>
    /// Gets the color picker
    /// </summary>
    /// <returns>The color picker</returns>
    public ColorPicker GetColorPicker()
    {
        return colorPicker;
    }

    /// <summary>
    /// Opens the settings
    /// </summary> 
    public void Open()
    {
        root.SetActive(true);
        SetCurrentTab(0);
    }

    /// <summary>
    /// Closes the options menu
    /// </summary>
    public void Close()
    {
        colorPicker.Close();
        root.SetActive(false);
    }

    /// <summary>
    /// Resets all settings
    /// </summary>
    public void ResetAllSettings()
    {
        Settings.instance.ResetAll();
        SetCurrentTab(currentTabIdx);
    }

    /// <summary>
    /// Changes the current tab
    /// </summary>
    /// <param name="tabIndex">The new tab's index/param>
    public void SetCurrentTab(int tabIndex)
    {
        currentTabIdx = tabIndex;
        EnableTab(tabs[tabIndex]);
    }

    /// <summary>
    /// Increment the current tab
    /// </summary>
    /// <param name="increment">The increment to apply</param>
    public void IncrementTab(int increment)
    {
        SetCurrentTab((currentTabIdx + increment + tabs.Length) % tabs.Length);
    }

    /// <summary>
    /// Enables a tab
    /// </summary>
    /// <param name="tab">The tab to enable</param>
    private void EnableTab(OptionsTab tab)
    {
        colorPicker.Close();
        if(currentTab) currentTab.Close();
        currentTab = tab;
        currentTab.Open(tabsRoot);
    }

    /// <summary>
    /// Hides the tipRoot
    /// </summary>
    public void HideTip()
    {
        tipRoot.gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows a tip
    /// </summary>
    /// <param name="tipID">The tip's ID</param>
    public void ShowTip(string tipID)
    {
        tipText.SetNewKey(tipID);
        tipRoot.gameObject.SetActive(true);
    }
}
