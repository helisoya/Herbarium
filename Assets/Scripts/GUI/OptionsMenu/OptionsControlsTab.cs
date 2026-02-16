using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the controls tab of the options
/// </summary>
public class OptionsControlsTab : OptionsTab
{
    [Header("Controls")]
    [SerializeField] private Toggle toggleModeGrabToggle;
    [SerializeField] private Toggle toggleModeMoveToggle;


    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
        toggleModeGrabToggle.SetIsOnWithoutNotify(Settings.instance.IsToggleGrabEnabled());
        toggleModeMoveToggle.SetIsOnWithoutNotify(Settings.instance.IsToggleMoveEnabled());
    }

    /// <summary>
    /// Callback for changing the toggle mode for micro interactions
    /// </summary>
    /// <param name="enabled">True if it is active</param>
    public void ChangeToggleGrabEnabled(bool enabled)
    {
        parent.InvokeOnCheckboxEvent(enabled);
        Settings.instance.EnableToggleGrab(enabled);
    }

    /// <summary>
    /// Callback for changing the toggle mode for moving
    /// </summary>
    /// <param name="enabled">True if it is active</param>
    public void ChangeToggleMoveEnabled(bool enabled)
    {
        parent.InvokeOnCheckboxEvent(enabled);
        Settings.instance.EnableToggleMove(enabled);
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
        Settings.instance.ResetInput();
        OnOpen();
    }
}
