using UnityEngine;
using UnityEditor;

/// <summary>
/// Represents the editor for the game's locals
/// </summary>
public class LocalsWindowEditor : EditorWindow
{
	[MenuItem("Herbarium/Locals")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(LocalsWindowEditor));
	}

	void OnGUI()
	{
		// The actual window code goes here
	}
}
