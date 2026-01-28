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
    [Header("Graphics")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Toggle hideHUDToggle;
    [SerializeField] private Toggle negativeColorFilterToggle;

    [Header("Player Hightlight")]
    [SerializeField] private Toggle playerHighlightToggle;
    [SerializeField] private Slider playerHighlightStrengthSlider;
    [SerializeField] private Image playerHighlightColorImage;

    [Header("Interactable Hightlight")]
    [SerializeField] private Toggle interactableHighlightToggle;
    [SerializeField] private Slider interactableHighlightStrengthSlider;
    [SerializeField] private Image interactableHighlightColorImage;


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

        playerHighlightToggle.SetIsOnWithoutNotify(Settings.instance.GetPlayerOutlineActive());
        playerHighlightStrengthSlider.SetValueWithoutNotify(Settings.instance.GetPlayerOutlineStrength());
        playerHighlightColorImage.color = Settings.instance.GetPlayerOutlineColor();
        playerHighlightStrengthSlider.interactable = playerHighlightToggle.isOn;
        playerHighlightColorImage.GetComponent<Button>().interactable = playerHighlightToggle.isOn;
        
        interactableHighlightToggle.SetIsOnWithoutNotify(Settings.instance.GetObjectOutlineActive());
        interactableHighlightStrengthSlider.SetValueWithoutNotify(Settings.instance.GetObjectsOutlineStrength());
        interactableHighlightColorImage.color = Settings.instance.GetObjectsOutlineColor();
        interactableHighlightStrengthSlider.interactable = interactableHighlightToggle.isOn;
        interactableHighlightColorImage.GetComponent<Button>().interactable = interactableHighlightToggle.isOn;
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

    /// <summary>
    /// Changes if the player highlight is enabled or not
    /// </summary>
    /// <param name="enabled">True if enabled</param>
    public void ChangePlayerHighlightEnabled(bool enabled)
    {
        Settings.instance.SetPlayerOutlineActive(enabled);
        playerHighlightStrengthSlider.interactable = enabled;
        playerHighlightColorImage.GetComponent<Button>().interactable = enabled;
    }

    /// <summary>
    /// Changes the player highlight's strength
    /// </summary>
    /// <param name="strength">The new strength</param>
    public void ChangePlayerHighlightStrength(float strength)
    {
        Settings.instance.SetPlayerOutlineStrength(strength);
    }

    /// <summary>
    /// Starts chaning the player highlight color
    /// </summary>
    public void StartChangingPlayerHighlightColor()
    {
        parent.GetColorPicker().Open(Settings.instance.GetPlayerOutlineColor(),ChangePlayerHightlightColor);
    }

    /// <summary>
    /// Changes the player highlight color
    /// </summary>
    /// <param name="color">The new color</param>
    public void ChangePlayerHightlightColor(Color color)
    {
        playerHighlightColorImage.color = color;
        Settings.instance.SetPlayerOutlineColor(color);
    }

    /// <summary>
    /// Changes if the objects highlight is enabled or not
    /// </summary>
    /// <param name="enabled">True if enabled</param>
    public void ChangeObjectsHighlightEnabled(bool enabled)
    {
        Settings.instance.SetObjectOutlineActive(enabled);
        interactableHighlightStrengthSlider.interactable = enabled;
        interactableHighlightColorImage.GetComponent<Button>().interactable = enabled;
    }

    /// <summary>
    /// Changes the objects highlight's strength
    /// </summary>
    /// <param name="strength">The new strength</param>
    public void ChangeObjectsHighlightStrength(float strength)
    {
        Settings.instance.SetObjectsOutlineStrength(strength);
    }

    /// <summary>
    /// Starts chaning the objects highlight color
    /// </summary>
    public void StartChangingObjectsHighlightColor()
    {
        parent.GetColorPicker().Open(Settings.instance.GetObjectsOutlineColor(),ChangeObjectsHightlightColor);
    }

    /// <summary>
    /// Changes the objects highlight color
    /// </summary>
    /// <param name="color">The new color</param>
    public void ChangeObjectsHightlightColor(Color color)
    {
        interactableHighlightColorImage.color = color;
        Settings.instance.SetObjectsOutlineColor(color);
    }

    /// <summary>
    /// Resets all settings
    /// </summary>
    public void ResetAll()
    {
        parent.GetConfirmPopup().Open(CallbackResetAll);
    }

    /// <summary>
    /// Callback for reseting all settings
    /// </summary>
    public void CallbackResetAll()
    {
        Settings.instance.ResetInterface();
        OnOpen();
    }
}
