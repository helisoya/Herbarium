using UnityEngine;
using UnityEngine.InputSystem.Utilities;

/// <summary>
/// Represents a plant's data
/// </summary>
[CreateAssetMenu(fileName = "Plant", menuName = "Herbarium/Plant")]
public class Plant : ScriptableObject
{
	public string id;
	public string categoryID;
	public Sprite herbariumSprite;

	public string Name { get { return id + "_Name"; } }
	public string LatinName { get { return id + "_LatinName"; } }
	public string Specifics { get { return id + "_Specifics"; } }
	public string Category {get{return "Category_"+categoryID;}}
	public string Lore { get { return id + "_Lore"; } }

	/// <summary>
	/// Gets an hint for the plant
	/// </summary>
	/// <param name="hintID">The hint's index (0,1,2)</param>
	/// <returns>The hint</returns>
	public string GetHint(int hintID)
	{
		return id+"_Hint_"+hintID;
	}
}
