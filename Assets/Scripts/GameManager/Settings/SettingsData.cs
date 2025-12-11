using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the setting's save data
/// </summary>
[CreateAssetMenu(fileName = "SettingsData", menuName = "Herbarium/Settings/SettingsData")]
public class SettingsData : ScriptableObject
{
	[Header("Gameplay")]
	public bool zoom;
	public bool autoCompleteInteraction;
	public bool soundGuidance;
	public bool audioDescription;
	public float screenShakeStrength;
	public float vibrationStrength;
	public bool playerCompass;
	public float gravity;

	[Header("Interface")]
	public float gamma;
	[HideInInspector] public uint refreshRateNumerator;
    [HideInInspector] public uint refreshRateDenominator;
    [HideInInspector] public int screenHeight;
    [HideInInspector] public int screenWidth;
    [HideInInspector] public bool fullscreen;
	public bool hideHUD;
	public int placementHUD;
	public int sizeHUD;
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
	public int outputDevice;
    

	[Header("Input")]
	public string remaping;
	public bool holdButtonEnabled;


	[Header("Text")]
    public string language;
	public int currentTypoIndexPrimary;
    public int primaryTextSize;
    public Color primaryTextColor;
	public bool subtitles;
	public int currentTypoIndexSecondary;
	public int secondaryTextSize;
    public Color secondaryTextColor;
	public float subtitlesBackgroundOpacity;

	public void Copy(SettingsData copy)
	{
		screenWidth = copy.screenWidth;
		screenHeight = copy.screenHeight;
		fullscreen = copy.fullscreen;
		refreshRateNumerator = copy.refreshRateNumerator;
		refreshRateDenominator = copy.refreshRateDenominator;
		language = copy.language;
		remaping = copy.remaping;
		currentTypoIndexPrimary = copy.currentTypoIndexPrimary;
		currentTypoIndexSecondary = copy.currentTypoIndexSecondary;
		volumeMaster = copy.volumeMaster;
		volumeSfx = copy.volumeSfx;
		volumeMusic = copy.volumeMusic;
		gamma = copy.gamma;
		outlinePlayer = copy.outlinePlayer;
		outlineObjects = copy.outlineObjects;
		zoom = copy.zoom;
		autoCompleteInteraction = copy.autoCompleteInteraction;
		soundGuidance = copy.soundGuidance;
		audioDescription = copy.audioDescription;
		screenShakeStrength = copy.screenShakeStrength;
		vibrationStrength = copy.vibrationStrength;
		playerCompass = copy.playerCompass;
		gravity = copy.gravity;
		hideHUD = copy.hideHUD;
		placementHUD = copy.placementHUD;
		sizeHUD = copy.sizeHUD;
		outlinePlayerStrength = copy.outlinePlayerStrength;
		outlinePlayerColor = copy.outlinePlayerColor;
		outlineObjects = copy.outlineObjects;
		outlineObjectsStrength = copy.outlineObjectsStrength;
		outlineObjectsColor = copy.outlineObjectsColor;
		negativeColorFilter = copy.negativeColorFilter;
		muteAll = copy.muteAll;
		outputDevice = copy.outputDevice;
		holdButtonEnabled = copy.holdButtonEnabled;
    	primaryTextSize = copy.primaryTextSize;
		primaryTextColor = copy.primaryTextColor;
		subtitles = copy.subtitles;
		secondaryTextColor = copy.secondaryTextColor;
		secondaryTextSize = copy.secondaryTextSize;
		subtitlesBackgroundOpacity = copy.subtitlesBackgroundOpacity;
	}
}
