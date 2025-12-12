using UnityEngine;
using UnityEngine.InputSystem.Utilities;

/// <summary>
/// Represents a plant's data
/// </summary>
[CreateAssetMenu(fileName = "Plant", menuName = "Herbarium/Plant")]
public class Plant : ScriptableObject
{
	public string id;

	public string Name { get { return id + "_Name"; } }
	public string Description { get { return id + "_Description"; } }
	public string Location { get { return id + "_Location"; } }
	public string Info { get { return id + "_Info"; } }
}
