using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the gameplay tab of the options
/// </summary>
public class OptionsGameplayTab : OptionsTab
{
    [Header("Gameplay")]
    [SerializeField] private Toggle autoCompleteToggle;
    [SerializeField] private Slider screenshakeStrength;


    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
        autoCompleteToggle.SetIsOnWithoutNotify(Settings.instance.IsAutocompleteInteractionEnabled());
        screenshakeStrength.SetValueWithoutNotify(Settings.instance.GetScreenshakeStrength());
    }

    /// <summary>
    /// Callback for changing the auto completion of the micro interactions settings
    /// </summary>
    /// <param name="enabled">True if it is active</param>
    public void ChangeAutoCompleteInteractionEnabled(bool enabled)
    {
        parent.InvokeOnCheckboxEvent(enabled);
        Settings.instance.EnableAutocompleteInteraction(enabled);
    }

    /// <summary>
    /// Callback for changing the screenshake's strength
    /// </summary>
    /// <param name="strength">The new strength</param>
    public void ChangeScreenshakeStrength(float strength)
    {
        parent.InvokeOnSliderEvent();
        Settings.instance.SetScreenshakeStrength(strength);
    }


    /// <summary>
    /// Resets all settings
    /// </summary>
    public void ResetAll()
    {
        parent.InvokeOnClickEvent();
        parent.GetConfirmPopup().Open(CallbackResetAll);
    }

    /// <summary>
    /// Callback for reseting all settings
    /// </summary>
    public void CallbackResetAll()
    {
        Settings.instance.ResetGameplay();
        OnOpen();
    }
}
