using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the interface tab of the options
/// </summary>
public class OptionsInterfaceTab : OptionsTab
{
    [Header("Interface")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Toggle hideHUDToggle;
    [SerializeField] private Toggle negativeColorFilterToggle;


    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
        negativeColorFilterToggle.SetIsOnWithoutNotify(Settings.instance.IsNegativeColorFilterEnabled());
        hideHUDToggle.SetIsOnWithoutNotify(!Settings.instance.IsHUDEnabled());
        fullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
        brightnessSlider.SetValueWithoutNotify(Settings.instance.GetCurrentGamma());

        int currentIdx = 0;
        Resolution[] resolutions = Screen.resolutions;
        Resolution currentRes = Screen.currentResolution;
        List<string> resolutionLabels = new List<string>();

        for(int i = 0; i < resolutions.Length; i++)
        {   
            //  +"("+System.Math.Round((float)resolutions[i].refreshRateRatio.numerator / resolutions[i].refreshRateRatio.denominator,2)+")"
            resolutionLabels.Add(resolutions[i].width+"x"+resolutions[i].height);

            if(currentIdx == 0 && currentRes.height == resolutions[i].height && currentRes.width == resolutions[i].width 
                && currentRes.refreshRateRatio.numerator == resolutions[i].refreshRateRatio.numerator && currentRes.refreshRateRatio.denominator == resolutions[i].refreshRateRatio.denominator)
            {
                currentIdx = i;
            }
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionLabels);
        resolutionDropdown.SetValueWithoutNotify(currentIdx);
    }

    /// <summary>
    /// Callback for changing the fullscreen settings
    /// </summary>
    /// <param name="fullScreen">True if in fullscreen</param>
    public void ChangeFullscreen(bool fullScreen)
    {
        Settings.instance.SetFullScreen(fullScreen);
    }

    /// <summary>
    /// Callback for changing the resolution
    /// </summary>
    /// <param name="resolutionIdx">The new resolution's index</param>
    public void ChangeResolution(int resolutionIdx)
    {
        Settings.instance.SetResolution(Screen.resolutions[resolutionIdx]);
    }

    /// <summary>
    /// Callback for changing the brightness
    /// </summary>
    /// <param name="brightness">The new brightness</param>
    public void ChangeBrightness(float brightness)
    {
        Settings.instance.SetGamma(brightness);
    }

    /// <summary>
    /// Callback for changing if the HUD is visible or not
    /// </summary>
    /// <param name="hidden">True if the HUD is hidden</param>
    public void ChangeHUDVisible(bool hidden)
    {
        Settings.instance.EnableHUD(!hidden);
    }

    /// <summary>
    /// Callback for changing if the negative color filter is enabled or not
    /// </summary>
    /// <param name="enabled">True if it is enabled</param>
    public void ChangeNegativeColorFilterEnabled(bool enabled)
    {
        Settings.instance.EnableNegativeColorFilter(enabled);
    }
}
