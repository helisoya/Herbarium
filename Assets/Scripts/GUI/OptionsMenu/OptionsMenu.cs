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
    private OptionsTab currentTab = null;
    private int currentTabIdx = 0;
    
    public bool isOpen{get{return root.activeInHierarchy;}}

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
        root.SetActive(false);
    }

    /// <summary>
    /// Resets all settings
    /// </summary>
    public void ResetAllSettings()
    {
        // Reset all or something
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
        if(currentTab) currentTab.Close();
        currentTab = tab;
        currentTab.Open(tabsRoot);
    }
}
