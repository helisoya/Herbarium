using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the setting's save data
/// </summary>
[CreateAssetMenu(fileName = "SettingsData", menuName = "Herbarium/Settings/SettingsData")]
public class SettingsData : ScriptableObject
{
	[Header("Gameplay")]
	public bool autoCompleteInteraction;
	public float screenShakeStrength;
	public float gravity;

	[Header("Interface")]
	public float gamma;
	[HideInInspector] public uint refreshRateNumerator;
	[HideInInspector] public uint refreshRateDenominator;
	[HideInInspector] public int screenHeight;
	[HideInInspector] public int screenWidth;
	[HideInInspector] public bool fullscreen;
	public bool hideHUD;
	public bool outlinePlayer;
	public float outlinePlayerStrength;
	public Color outlinePlayerColor;
	public bool outlineObjects;
	public float outlineObjectsStrength;
	public Color outlineObjectsColor;
	public bool negativeColorFilter;

	[Header("Sound")]
	public float volumeMaster;
	public float volumeMusic;
	public float volumeSfx;
	public bool muteAll;
	public bool isMono;


	[Header("Input")]
	public string remaping;
	public bool toggleGrabEnabled;
	public bool toggleMoveEnabled;


	[Header("Text")]
	public string language;
	public float subtitlesBackgroundOpacity;
	public LocalChannelData[] textChannelsDatas;

	public void Copy(SettingsData copy)
	{
		screenWidth = copy.screenWidth;
		screenHeight = copy.screenHeight;
		fullscreen = copy.fullscreen;
		refreshRateNumerator = copy.refreshRateNumerator;
		refreshRateDenominator = copy.refreshRateDenominator;
		language = copy.language;
		remaping = copy.remaping;
		textChannelsDatas = copy.textChannelsDatas;
		volumeMaster = copy.volumeMaster;
		volumeSfx = copy.volumeSfx;
		volumeMusic = copy.volumeMusic;
		gamma = copy.gamma;
		outlinePlayer = copy.outlinePlayer;
		outlineObjects = copy.outlineObjects;
		autoCompleteInteraction = copy.autoCompleteInteraction;
		gravity = copy.gravity;
		hideHUD = copy.hideHUD;
		outlinePlayerStrength = copy.outlinePlayerStrength;
		outlinePlayerColor = copy.outlinePlayerColor;
		outlineObjectsStrength = copy.outlineObjectsStrength;
		outlineObjectsColor = copy.outlineObjectsColor;
		negativeColorFilter = copy.negativeColorFilter;
		muteAll = copy.muteAll;
		isMono = copy.isMono;
		toggleGrabEnabled = copy.toggleGrabEnabled;
		subtitlesBackgroundOpacity = copy.subtitlesBackgroundOpacity;
		toggleMoveEnabled = copy.toggleMoveEnabled;
	}
}
