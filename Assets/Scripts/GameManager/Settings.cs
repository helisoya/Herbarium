using UnityEngine;

/// <summary>
/// Handles the settings of the game
/// </summary>
public class Settings
{
    private static Settings instance;
    private SettingsData data;
    private SettingsData defaultData;

    /// <summary>
    /// Initiliazes the Locals
    /// </summary>
    public static void Init()
    {
        new Settings();
    }

    public Settings()
    {
        instance = this;
        defaultData = Resources.Load<SettingsData>("Data/SO_SettingsData");
        data = SettingsData.CreateInstance<SettingsData>();
        // Set "dynamic" default data (remaping, ...)

        if (fileExistsOnDisk)
        {
            Load();
        }
        else
        {
            data.Copy(defaultData);
            InititiazeData();
            Save();
        }
    }



    public string filePath
    {
        get
        {
            return FileManager.savPath + "settings.sav";
        }
    }

    public bool fileExistsOnDisk
    {
        get
        {
            return System.IO.File.Exists(filePath);
        }
    }

    #region Gameplay

    /// <summary>
    /// Enables or disables the zoom
    /// </summary>
    /// <param name="active">True if enabled</param>
    public void EnableZoom(bool active)
    {
        data.zoom = active;
        Save();
    }

    /// <summary>
    /// Gets if the zoom is enabled
    /// </summary>
    /// <returns>True if enabled</returns>
    public bool IsZoomEnabled()
    {
        return data.zoom;
    }

    /// <summary>
    /// Enables or disables the auto completion of interactions
    /// </summary>
    /// <param name="active">True if enabled</param>
    public void EnableAutocompleteInteraction(bool active)
    {
        data.autoCompleteInteraction = active;
        Save();
    }

    /// <summary>
    /// Gets if the auto completion of interactions is enabled
    /// </summary>
    /// <returns>True if enabled</returns>
    public bool IsAutocompleteInteractionEnabled()
    {
        return data.autoCompleteInteraction;
    }

    /// <summary>
    /// Enables or disables the sound guidance
    /// </summary>
    /// <param name="active">True if enabled</param>
    public void EnableSoundGuidance(bool active)
    {
        data.soundGuidance = active;
        Save();
    }

    /// <summary>
    /// Gets if the sound guidance is enabled
    /// </summary>
    /// <returns>True if enabled</returns>
    public bool IsSoundGuidanceEnabled()
    {
        return data.soundGuidance;
    }

    /// <summary>
    /// Enables or disables the audio description
    /// </summary>
    /// <param name="active">True if enabled</param>
    public void EnableAudioDescription(bool active)
    {
        data.audioDescription = active;
        Save();
    }

    /// <summary>
    /// Gets if the audio description is enabled
    /// </summary>
    /// <returns>True if enabled</returns>
    public bool IsAudioDescriptionEnabled()
    {
        return data.audioDescription;
    }

    /// <summary>
    /// Sets the screenshake strength
    /// </summary>
    /// <param name="value">The strength value</param>
    public void SetScreenshakeStrength(float value)
    {
        data.screenShakeStrength = value;
        Save();
    }

    /// <summary>
    /// Gets the screenshake strength
    /// </summary>
    /// <returns>The strength value</returns>
    public float GetScreenshakeStrength()
    {
        return data.screenShakeStrength;
    }

    /// <summary>
    /// Sets the vibration strength
    /// </summary>
    /// <param name="value">The strength value</param>
    public void SetVibrationStrength(float value)
    {
        data.vibrationStrength = value;
        Save();
    }

    /// <summary>
    /// Gets the vibration strength
    /// </summary>
    /// <returns>The strength value</returns>
    public float GetVibrationStrength()
    {
        return data.vibrationStrength;
    }

    /// <summary>
    /// Enables or disables the player compass
    /// </summary>
    /// <param name="active">True if enabled</param>
    public void EnablePlayerCompass(bool active)
    {
        data.playerCompass = active;
        Save();
    }

    /// <summary>
    /// Gets if the player compass is enabled
    /// </summary>
    /// <returns>True if enabled</returns>
    public bool IsPlayerCompassEnabled()
    {
        return data.playerCompass;
    }

    /// <summary>
    /// Sets the gravity
    /// </summary>
    /// <param name="value">The gravity value</param>
    public void SetGravity(float value)
    {
        data.gravity = value;
        Save();
    }

    /// <summary>
    /// Gets the gravity
    /// </summary>
    /// <returns>The gravity value</returns>
    public float GetGravity()
    {
        return data.gravity;
    }

    #endregion

    #region Interface

    /// <summary>
    /// Changes if the game is in fullscreen or not
    /// </summary>
    /// <param name="isFullScreen">True if the game is in fullscreen</param>
    public void SetFullScreen(bool isFullScreen)
    {
        data.fullscreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
        Save();
    }

    /// <summary>
    /// Changes the game's resolution
    /// </summary>
    /// <param name="newResolution">The new resolution</param>
    public void SetResolution(Resolution newResolution)
    {
        data.refreshRateNumerator = newResolution.refreshRateRatio.numerator;
        data.refreshRateDenominator = newResolution.refreshRateRatio.denominator;
        data.screenWidth = newResolution.width;
        data.screenHeight = newResolution.height;
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreenMode, newResolution.refreshRateRatio);
        Save();
    }


    /// <summary>
    /// Gets the current gamma
    /// </summary>
    /// <returns>The current gama</returns>
    public float GetCurrentGamma()
    {
        return data.gamma;
    }

    /// <summary>
    /// Sets the current gamma
    /// </summary>
    /// <param name="gamma">The new gamma</param>
    public void SetGamma(float gamma){
        data.gamma = gamma;
        //GameManager.instance.UpdateVolume();
        Save();
    }



    /// <summary>
    /// Enables or disables the HUD
    /// </summary>
    /// <param name="active">True if enabled</param>
    public void EnableHUD(bool active)
    {
        data.hideHUD = !active;
        Save();
    }

    /// <summary>
    /// Gets if the HUD is enabled
    /// </summary>
    /// <returns>True if enabled</returns>
    public bool IsHUDEnabled()
    {
        return !data.hideHUD;
    }

    /// <summary>
    /// Sets the HUD placement
    /// </summary>
    /// <param name="value">The HUD placement index</param>
    public void SetHUDPlacement(int value)
    {
        data.placementHUD = value;
        Save();
    }

    /// <summary>
    /// Gets the HUD placement index
    /// </summary>
    /// <returns>The HUD placement</returns>
    public int GetHUDPlacement()
    {
        return data.placementHUD;
    }

    /// <summary>
    /// Sets the HUD size
    /// </summary>
    /// <param name="value">The HUD size index</param>
    public void SetHUDSize(int value)
    {
        data.sizeHUD = value;
        Save();
    }

    /// <summary>
    /// Gets the HUD size index
    /// </summary>
    /// <returns>The HUD size</returns>
    public int GetHUDSize()
    {
        return data.sizeHUD;
    }

    /// <summary>
    /// Changes the player outline color
    /// </summary>
    /// <param name="color">The player outline color</param>
    public void SetPlayerOutlineColor(Color color)
    {
        data.outlinePlayerColor = color;
        Save();
    }

    /// <summary>
    /// Gets the player outline's color
    /// </summary>
    /// <returns>The player outline's color</returns>
    public Color GetPlayerOutlineColor()
    {
        return data.outlinePlayerColor;
    }

    /// <summary>
    /// Changes the player outline strength
    /// </summary>
    /// <param name="strength">The player outline strength</param>
    public void SetPlayerOutlineStrength(float strength)
    {
        data.outlinePlayerStrength = strength;
        Save();
    }

    /// <summary>
    /// Gets the player outline's strength
    /// </summary>
    /// <returns>The player outline's strength</returns>
    public float GetPlayerOutlineStrength()
    {
        return data.outlinePlayerStrength;
    }

    /// <summary>
    /// Changes the objects outline color
    /// </summary>
    /// <param name="color">The objects outline color</param>
    public void SetObjectsOutlineColor(Color color)
    {
        data.outlineObjectsColor = color;
        Save();
    }

    /// <summary>
    /// Gets the objects outline's color
    /// </summary>
    /// <returns>The objects outline's color</returns>
    public Color GetObjectsOutlineColor()
    {
        return data.outlineObjectsColor;
    }

    /// <summary>
    /// Changes the objects outline strength
    /// </summary>
    /// <param name="strength">The objects outline strength</param>
    public void SetObjectsOutlineStrength(float strength)
    {
        data.outlineObjectsStrength = strength;
        Save();
    }

    /// <summary>
    /// Gets the objects outline's strength
    /// </summary>
    /// <returns>The objects outline's strength</returns>
    public float GetObjectsOutlineStrength()
    {
        return data.outlineObjectsStrength;
    }

    /// <summary>
    /// Changes if the player outline is active or not
    /// </summary>
    /// <param name="active">True if active</param>
    public void SetPlayerOutlineActive(bool active)
    {
        data.outlinePlayer = active;
        Save();
    }

    /// <summary>
    /// Gets if the player outline is active or not
    /// </summary>
    /// <returns>True if the player outline is active</returns>
    public bool GetPlayerOutlineActive()
    {
        return data.outlinePlayer;
    }

    /// <summary>
    /// Changes if the objects outline is active or not
    /// </summary>
    /// <param name="active">True if active</param>
    public void SetObjectOutlineActive(bool active)
    {
        data.outlineObjects = active;
        Save();
    }

    /// <summary>
    /// Gets if the objects outline is active or not
    /// </summary>
    /// <returns>True if the objects outline is active</returns>
    public bool GetObjectOutlineActive()
    {
        return data.outlineObjects;
    }

    /// <summary>
    /// Changes if the negative filter is active or not
    /// </summary>
    /// <param name="active">True if active</param>
    public void EnableNegativeColorFilter(bool active)
    {
        data.negativeColorFilter = active;
        Save();
    }

    /// <summary>
    /// Gets if the negative filter is active or not
    /// </summary>
    /// <returns>True if the negative filter is active</returns>
    public bool IsNegativeColorFilterEnabled()
    {
        return data.negativeColorFilter;
    }


    #endregion

    #region Sound

    /// <summary>
    /// Sets the master's volume
    /// </summary>
    /// <param name="value">The new volume</param>
    public void SetVolumeMaster(float value)
    {
        data.volumeMaster = value;
        Save();
    }

    /// <summary>
    /// Gets the master's volume
    /// </summary>
    /// <returns>The master's volume</returns>
    public float GetVolumeMaster()
    {
        return data.volumeMaster;
    }

    /// <summary>
    /// Sets the music's volume
    /// </summary>
    /// <param name="value">The new volume</param>
    public void SetVolumeMusic(float value)
    {
        data.volumeMusic = value;
        Save();
    }

    /// <summary>
    /// Gets the music's volume
    /// </summary>
    /// <returns>The music's volume</returns>
    public float GetVolumeMusic()
    {
        return data.volumeMusic;
    }

    /// <summary>
    /// Sets the SFX's volume
    /// </summary>
    /// <param name="value">The new volume</param>
    public void SetVolumeSFX(float value)
    {
        data.volumeSfx = value;
        Save();
    }

    /// <summary>
    /// Gets the SFX's volume
    /// </summary>
    /// <returns>The SFX's volume</returns>
    public float GetVolumeSFX()
    {
        return data.volumeSfx;
    }

    /// <summary>
    /// Changes if the sounds are muted or not
    /// </summary>
    /// <param name="active">True if muted</param>
    public void MuteAllSounds(bool active)
    {
        data.muteAll = active;
        Save();
    }

    /// <summary>
    /// Gets if the sounds are muted
    /// </summary>
    /// <returns>True if the sounds are muted</returns>
    public bool IsSoundsMuted()
    {
        return data.muteAll;
    }

    /// <summary>
    /// Sets the output device
    /// </summary>
    /// <param name="index">The output device index</param>
    public void SetOutputDevice(int index)
    {
        data.outputDevice = index;
        Save();
    }

    /// <summary>
    /// Gets the Output device
    /// </summary>
    /// <returns>The output device index</returns>
    public int GetOutputDevice()
    {
        return data.outputDevice;
    }

    #endregion

    #region Input

    /// <summary>
    /// Save the game's bindings
    /// </summary>
    /// <param name="bindings">The new bindings (JSON)</param>
    public void SetBindings(string bindings)
    {
        data.remaping = bindings;
        //if(!GameManager.instance.inMainMenu) Player.instance.RefreshBindings();
        Save();
    }

    /// <summary>
    /// Changes if the "hold mode" is active or not
    /// </summary>
    /// <param name="active">True if active</param>
    public void EnableHoldMode(bool active)
    {
        data.holdButtonEnabled = active;
        Save();
    }

    /// <summary>
    /// Gets if the "hold mode" is active or not
    /// </summary>
    /// <returns>True if the "hold mode" is active</returns>
    public bool IsHoldModeEnabled()
    {
        return data.holdButtonEnabled;
    }


    #endregion

    #region Text

    /// <summary>
    /// Changes the current language
    /// </summary>
    /// <param name="newLanguage">The new language</param>
    public void ChangeLanguage(string newLanguage)
    {
        data.language = newLanguage;
        Locals.ChangeLanguage(newLanguage);
        Save();
    }

    /// <summary>
    /// Save the current primary typo index
    /// </summary>
    /// <param name="newIdx">The new typo index</param>
    public void SetCurrentTypoIndexPrimary(int newIdx)
    {
        data.currentTypoIndexPrimary = newIdx;
        Locals.ChangeFontPrimary(newIdx);
        Save();
    }

    /// <summary>
    /// Get the current primary typo index
    /// </summary>
    /// <returns>The current typo index</returns>
    public int GetCurrentTypoIndexPrimary()
    {
        return data.currentTypoIndexPrimary;
    }
    
    /// <summary>
    /// Save the current secondary typo index
    /// </summary>
    /// <param name="newIdx">The new typo index</param>
    public void SetCurrentTypoIndexSecondary(int newIdx)
    {
        data.currentTypoIndexSecondary = newIdx;
        Locals.ChangeFontSecondary(newIdx);
        Save();
    }

    /// <summary>
    /// Get the current secondary typo index
    /// </summary>
    /// <returns>The current typo index</returns>
    public int GetCurrentTypoIndexSecondary()
    {
        return data.currentTypoIndexSecondary;
    }

 
    /// <summary>
    /// Sets the primary text size
    /// </summary>
    /// <param name="size">The size index</param>
    public void SetPrimaryTextSize(int size){
        data.primaryTextSize = size;
        Locals.ChangeSizePrimary(size);
        Save();
    }

    /// <summary>
    /// Gets the primary text size index
    /// </summary>
    /// <returns>The size index</returns>
    public int GetPrimaryTextSize(){
        return data.primaryTextSize;
    }

    /// <summary>
    /// Sets the secondary text size
    /// </summary>
    /// <param name="size">The size index</param>
    public void SetSecondaryTextSize(int size){
        data.secondaryTextSize = size;
        Locals.ChangeSizeSecondary(size);
        Save();
    }

    /// <summary>
    /// Gets the secondary text size index
    /// </summary>
    /// <returns>The size index</returns>
    public int GetSecondaryTextSize(){
        return data.secondaryTextSize;
    }

    /// <summary>
    /// Sets the game's test opacity index
    /// </summary>
    /// <param name="opacity">The opacity index</param>
    public void SetSubtitlesBackgroundOpacity(float opacity){
        data.subtitlesBackgroundOpacity = opacity;
        //if(!GameManager.instance.inMainMenu) GameGUI.instance.SetDialogBackgroundAlpha(opacity);
        Save();
    }

    /// <summary>
    /// Gets the game's text opacity index
    /// </summary>
    /// <returns>The opacity index</returns>
    public float GetSubtitlesBackgroundOpacity(){
        return data.subtitlesBackgroundOpacity;
    }

   /// <summary>
    /// Changes if the subtitles are active or not
    /// </summary>
    /// <param name="active">True if active</param>
    public void EnableSubtitles(bool active)
    {
        data.subtitles = active;
        Save();
    }

    /// <summary>
    /// Gets if the subtitles are active or not
    /// </summary>
    /// <returns>True if the "hold mode" is active</returns>
    public bool IsSubtitlesEnabled()
    {
        return data.subtitles;
    }

    /// <summary>
    /// Sets the secondary text color
    /// </summary>
    /// <param name="color">The color</param>
    public void SetSecondaryColor(Color color){
        data.secondaryTextColor = color;
        Locals.ChangeColorSecondary(color);
        Save();
    }

    /// <summary>
    /// Gets the secondary text color
    /// </summary>
    /// <returns>The color</returns>
    public Color GetSecondaryTextColor(){
        return data.secondaryTextColor;
    }

    /// <summary>
    /// Sets the primary text color
    /// </summary>
    /// <param name="color">The color</param>
    public void SetPrimaryColor(Color color){
        data.primaryTextColor = color;
        Locals.ChangeColorPrimary(color);
        Save();
    }

    /// <summary>
    /// Gets the primary text color
    /// </summary>
    /// <returns>The color</returns>
    public Color GetPrimaryTextColor(){
        return data.primaryTextColor;
    }

    #endregion


    #region Resets

    /// <summary>
    /// Initialize the save data that can't be specified in inspector
    /// Ex : Fullscreen, screen resolution, ...
    /// </summary>
    public void InititiazeData()
    {
        data.fullscreen = Screen.fullScreen;
        data.refreshRateDenominator = Screen.currentResolution.refreshRateRatio.denominator;
        data.refreshRateNumerator = Screen.currentResolution.refreshRateRatio.numerator;
        data.screenHeight = Screen.currentResolution.height;
        data.screenWidth = Screen.currentResolution.width;
        
        Locals.ChangeLanguage(defaultData.language);
        Locals.ChangeColorPrimary(defaultData.primaryTextColor);
        Locals.ChangeColorSecondary(defaultData.secondaryTextColor);
        Locals.ChangeFontPrimary(defaultData.currentTypoIndexPrimary);
        Locals.ChangeFontSecondary(defaultData.currentTypoIndexSecondary);
        Locals.ChangeSizePrimary(defaultData.primaryTextSize);
        Locals.ChangeSizeSecondary(defaultData.secondaryTextSize);
    }

    #endregion


    /// <summary>
    /// Loads the settings from disk
    /// </summary>
    private void Load()
    {
        FileManager.LoadJSON<SettingsData>(filePath,ref data);

        RefreshRate refreshRate = new RefreshRate
        {
            denominator = data.refreshRateDenominator,
            numerator = data.refreshRateNumerator
        };

        Screen.SetResolution(data.screenWidth, data.screenHeight, Screen.fullScreenMode, refreshRate);
        Locals.ChangeLanguage(data.language);
        Screen.fullScreen = data.fullscreen;
        //GameManager.instance.GetInputs().LoadBindingOverridesFromJson(data.remaping);
        //GameManager.instance.UpdateVolume();
        Locals.ChangeFontPrimary(data.currentTypoIndexPrimary);
        Locals.ChangeColorPrimary(data.primaryTextColor);
        Locals.ChangeSizePrimary(data.primaryTextSize);

        Locals.ChangeFontSecondary(data.currentTypoIndexSecondary);
        Locals.ChangeColorSecondary(data.secondaryTextColor);
        Locals.ChangeSizeSecondary(data.secondaryTextSize);
    }

    /// <summary>
    /// Saves the settings to disk
    /// </summary>
    private void Save()
    {
        FileManager.SaveJSON(filePath, data);
    }  

}