using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

/// <summary>
/// Represents a plant's data
/// </summary>
[CreateAssetMenu(fileName = "Plant", menuName = "Herbarium/Plant")]
public class Plant : ScriptableObject
{
	[Header("General")]
	public string id;
	public string categoryID;
	public bool isSecret = false;

	[Header("Radial Menu")]
	public Sprite radialMenuSprite;

	[Header("Herbarium")]
	public Sprite driedSprite;
	public Sprite shadowSprite;
	public string Category {get{return "Category_"+categoryID;}}

	[Header("Foraging")]
	public Transform foragingPrefab;
	public int foragingHealth;

	/// <summary>
	/// Gets an hint for the plant
	/// </summary>
	/// <param name="plantId">The plant ID</param>
	/// <param name="hintID">The hint's index (0,1,2)</param>
	/// <returns>The hint</returns>
	public static string GetHint(string plantId, int hintID)
	{
		return plantId+"_Hint_"+hintID;
	}

	/// <summary>
	/// Gets the lore of a plant
	/// </summary>
	/// <param name="plantId">The plant ID</param>
	/// <returns>The lore</returns>
	public static string GetLore(string plantId)
	{
		return plantId+"_Lore";
	}

	/// <summary>
	/// Gets the specifics of a plant
	/// </summary>
	/// <param name="plantId">The plant ID</param>
	/// <returns>The specifics</returns>
	public static string GetSpecifics(string plantId)
	{
		return plantId+"_Specifics";
	}

	/// <summary>
	/// Gets the name of a plant
	/// </summary>
	/// <param name="plantId">The plant ID</param>
	/// <returns>The name</returns>
	public static string GetName(string plantId)
	{
		return plantId+"_Name";
	}

	/// <summary>
	/// Gets the latin name of a plant
	/// </summary>
	/// <param name="plantId">The plant ID</param>
	/// <returns>The latin name</returns>
	public static string GetLatinName(string plantId)
	{
		return plantId+"_LatinName";
	}
}
