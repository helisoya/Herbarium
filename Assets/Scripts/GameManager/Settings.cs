using UnityEngine;

/// <summary>
/// Handles the settings of the game
/// </summary>
public class Settings
{
    private static Settings self;
    private SettingsData data;
    private SettingsData defaultData;

    /// <summary>
    /// Initiliazes the Locals
    /// </summary>
    public static void Init()
    {
        new Locals();
    }

    public Settings()
    {
        self = this;
        defaultData = Resources.Load<SettingsData>("Data/SO_SettingsData");
        data = new SettingsData(defaultData);
    }
}
