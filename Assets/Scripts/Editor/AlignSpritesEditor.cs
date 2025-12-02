using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

/// <summary>
/// Editor for aligning sprites
/// </summary>
[CustomEditor(typeof(AlignSprites))]
public class AlignSpritesEditor : Editor
{
    SerializedObject obj;
    SerializedProperty sprites;
    SerializedProperty slider;

    [MenuItem("GameObject/Herbarium/Aligned Sprites")]
    static void CreateAsset()
    {
        GameObject obj = new GameObject();
        obj.name = "AlignedSprites";
        obj.AddComponent<AlignSprites>();
    }

    private void Init()
    {
        obj = new SerializedObject(target);
        sprites = obj.FindProperty("sprites");
        slider = obj.FindProperty("slider");
    }

    public override void OnInspectorGUI(){
        if(obj == null) Init();

        EditorGUILayout.PropertyField(sprites);
        EditorGUILayout.PropertyField(slider);

        if (GUILayout.Button("Reset Sprites"))
        {
            AlignSprites value = (AlignSprites)target;

            while(value.transform.childCount > 0) DestroyImmediate(value.transform.GetChild(0).gameObject);
            foreach(Transform child in value.transform) DestroyImmediate(child.gameObject);
            for(int i = 0; i < value.sprites.Length; i++)
            {
                GameObject obj = new GameObject("Sprite-"+i);
                obj.transform.SetParent(value.transform);
                obj.AddComponent<SpriteRenderer>().sprite = value.sprites[i];
                obj.transform.localPosition = new Vector3(0,0,value.slider * i);
            }
        }

        obj.ApplyModifiedProperties();
    }
}
