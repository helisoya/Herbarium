using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.IO;
using System;

/// <summary>
/// Represents the editor for the game's locals
/// </summary>
public class LocalsWindowEditor : EditorWindow
{
	private LocalsData data;
	private SerializedObject serializedData;
	private SerializedProperty fontsProperty;
	private bool foldoutAdd = false;
	private string textAdd = "";
	private bool[] foldouts;

	public void Init()
	{
		data = Resources.Load<LocalsData>("Data/SO_LocalsData");
		foldouts = new bool[data.languages.Length];
		serializedData = new SerializedObject(data);
		fontsProperty = serializedData.FindProperty("fonts");
	}

	[MenuItem("Herbarium/Locals")]
	public static void ShowExample()
	{
		LocalsWindowEditor wnd = GetWindow<LocalsWindowEditor>();
		wnd.titleContent = new GUIContent("Locals");
	}

	public void OnGUI()
	{
		if (!data) Init();
		serializedData.Update();

		for (int i = 0; i < data.languages.Length; i++)
		{
			foldouts[i] = EditorGUILayout.Foldout(foldouts[i], data.languages[i]);
			if (foldouts[i])
			{
				if (GUILayout.Button("Edit story"))
				{
					System.Diagnostics.Process.Start("devenv", "Assets/Resources/Locals/" + data.languages[i] + "_story.txt");
				}

				if (GUILayout.Button("Edit system"))
				{
					System.Diagnostics.Process.Start("devenv", "Assets/Resources/Locals/" + data.languages[i] + "_system.txt");
				}

				if (GUILayout.Button("Delete local"))
				{
					AssetDatabase.DeleteAsset("Assets/Resources/Locals/" + data.languages[i] + "_story.txt");
					AssetDatabase.DeleteAsset("Assets/Resources/Locals/" + data.languages[i] + "_system.txt");
					AssetDatabase.SaveAssets();

					DeleteIndex(ref data.languages, i);
					DeleteIndex(ref foldouts, i);
					return;
				}
			}
		}

		GUILayout.Space(30);
		foldoutAdd = EditorGUILayout.Foldout(foldoutAdd, "Add new local");
		if (foldoutAdd)
		{
			EditorGUILayout.LabelField("Language ID");
			textAdd = EditorGUILayout.TextArea(textAdd);
			if (GUILayout.Button("Add") && LocalDoesNotExist(textAdd))
			{
				Array.Resize(ref data.languages, data.languages.Length + 1);
				Array.Resize(ref foldouts, foldouts.Length + 1);
				data.languages[data.languages.Length - 1] = textAdd;
				foldouts[foldouts.Length - 1] = true;

				StreamWriter writer = new StreamWriter("Assets/Resources/Locals/" + textAdd + "_story.txt");
				writer.WriteLine("# " + textAdd);
				writer.Close();

				writer = new StreamWriter("Assets/Resources/Locals/" + textAdd + "_system.txt");
				writer.WriteLine("# " + textAdd);
				writer.Close();
				AssetDatabase.SaveAssets();
			}
		}

		GUILayout.Space(30);
		EditorGUILayout.LabelField("Fonts");
		EditorGUILayout.PropertyField(fontsProperty);
		serializedData.ApplyModifiedProperties();

		EditorUtility.SetDirty(data);
	}

	private bool LocalDoesNotExist(string local)
	{
		foreach (string language in data.languages)
		{
			if (language.Equals(local)) return false;
		}
		return true;
	}

	private void DeleteIndex<T>(ref T[] array, int indexToDelete)
	{
		if (array.Length == 0 || indexToDelete < 0 || indexToDelete >= array.Length) return;
		for (int i = indexToDelete + 1; i < array.Length; i++)
		{
			array[i - 1] = array[i];
		}

		Array.Resize(ref array, array.Length - 1);
	}
}
