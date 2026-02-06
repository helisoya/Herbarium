using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the sound tab of the options
/// </summary>
public class OptionsSoundTab : OptionsTab
{
    [Header("Sound")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle muteAllSlider;
    [SerializeField] private Toggle mixToggle;


    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
        bool allMuted = Settings.instance.IsSoundsMuted();
        masterSlider.SetValueWithoutNotify(Settings.instance.GetVolumeMaster());
        musicSlider.SetValueWithoutNotify(Settings.instance.GetVolumeMusic());
        sfxSlider.SetValueWithoutNotify(Settings.instance.GetVolumeSFX());
        muteAllSlider.SetIsOnWithoutNotify(allMuted);

        masterSlider.interactable = !allMuted;
        sfxSlider.interactable = !allMuted;
        musicSlider.interactable = !allMuted;

        mixToggle.SetIsOnWithoutNotify(Settings.instance.GetIsMono());
    }

    /// <summary>
    /// Callback for changing the master volume
    /// </summary>
    /// <param name="volume">The new volume</param>
    public void ChangeMasterVolume(float volume)
    {
        parent.InvokeOnSliderEvent();
        Settings.instance.SetVolumeMaster(volume);
    }

    /// <summary>
    /// Callback for changing the SFX volume
    /// </summary>
    /// <param name="volume">The new volume</param>
    public void ChangeSFXVolume(float volume)
    {
        parent.InvokeOnSliderEvent();
        Settings.instance.SetVolumeSFX(volume);
    }

    /// <summary>
    /// Callback for changing the Music volume
    /// </summary>
    /// <param name="volume">The new volume</param>
    public void ChangeMusicVolume(float volume)
    {
        parent.InvokeOnSliderEvent();
        Settings.instance.SetVolumeMusic(volume);
    }

    /// <summary>
    /// Callback for muting the sounds
    /// </summary>
    /// <param name="muted">True if all sounds are muted</param>
    public void ChangeSoundsMuted(bool muted)
    {
        parent.InvokeOnCheckboxEvent(muted);
        Settings.instance.MuteAllSounds(muted);
        OnOpen();
    }

    /// <summary>
    /// Callback for changing if audio is mono or not
    /// </summary>
    /// <param name="isMono">True if the audio is in mono</param>
    public void ChangeIsMono(bool isMono)
    {
        parent.InvokeOnCheckboxEvent(isMono);
        Settings.instance.SetIsMono(isMono);
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
        Settings.instance.ResetSound();
        OnOpen();
    }
}
