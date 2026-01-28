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
    [SerializeField] private TMP_Dropdown deviceDropdown;


    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
        masterSlider.SetValueWithoutNotify(Settings.instance.GetVolumeMaster());
        musicSlider.SetValueWithoutNotify(Settings.instance.GetVolumeMusic());
        sfxSlider.SetValueWithoutNotify(Settings.instance.GetVolumeSFX());
        muteAllSlider.SetIsOnWithoutNotify(Settings.instance.IsSoundsMuted());

        deviceDropdown.ClearOptions();
    }

    /// <summary>
    /// Callback for changing the master volume
    /// </summary>
    /// <param name="volume">The new volume</param>
    public void ChangeMasterVolume(float volume)
    {
        Settings.instance.SetVolumeMaster(volume);
    }

    /// <summary>
    /// Callback for changing the SFX volume
    /// </summary>
    /// <param name="volume">The new volume</param>
    public void ChangeSFXVolume(float volume)
    {
        Settings.instance.SetVolumeSFX(volume);
    }

    /// <summary>
    /// Callback for changing the Music volume
    /// </summary>
    /// <param name="volume">The new volume</param>
    public void ChangeMusicVolume(float volume)
    {
        Settings.instance.SetVolumeMusic(volume);
    }

    /// <summary>
    /// Callback for muting the sounds
    /// </summary>
    /// <param name="muted">True if all sounds are muted</param>
    public void ChangeSoundsMuted(bool muted)
    {
        Settings.instance.MuteAllSounds(muted);
    }

    /// <summary>
    /// Callback for changing the output device
    /// </summary>
    /// <param name="deviceIdx">The device's index</param>
    public void ChangeOutputDevice(int deviceIdx)
    {
        Settings.instance.SetOutputDevice(deviceIdx);
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
        Settings.instance.ResetSound();
        OnOpen();
    }
}
