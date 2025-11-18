using UnityEngine;

/// <summary>
/// Represents the setting's save data
/// </summary>
[CreateAssetMenu(fileName = "SettingsData", menuName = "Herbarium/Settings/SettingsData")]
public class SettingsData : ScriptableObject
{
	public int screenWidth;
	public int screenHeight;
	public bool fullscreen;

	public SettingsData(SettingsData copy)
	{
		screenWidth = copy.screenWidth;
		screenHeight = copy.screenHeight;
		fullscreen = copy.fullscreen;
	}
}
