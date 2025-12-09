using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using XNodeEditor;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

/// <summary>
/// Editor for aligning sprites
/// </summary>
[CustomEditor(typeof(AlignSprites))]
public class AlignSpritesEditor : Editor
{
    SerializedObject obj;
    SerializedProperty sprites;
    SerializedProperty displacement;
    SerializedProperty rotation;

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
        displacement = obj.FindProperty("displacement");
        rotation = obj.FindProperty("rotation");
    }

    public override void OnInspectorGUI(){
        if(obj == null) Init();

        EditorGUILayout.PropertyField(sprites);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(displacement);

        if (GUILayout.Button("Reset Sprites"))
        {
            AlignSprites value = (AlignSprites)target;

            DestroyInstantiatedSprites();
            for(int i = 0; i < value.sprites.Length; i++)
            {
                GameObject obj = new GameObject("Sprite-"+i);
                obj.transform.SetParent(value.transform);
                obj.AddComponent<SpriteRenderer>().sprite = value.sprites[i];
                obj.transform.localPosition = new Vector3(0,0,value.displacement * i);
                obj.transform.localRotation = Quaternion.identity;
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(rotation);

        if (GUILayout.Button("Apply Rotation"))
        {
            RotateInstantiatedSprites();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Add trigger"))
        {
            AlignSprites value = (AlignSprites)target;

            GameObject obj = new GameObject("Trigger");
            obj.transform.SetParent(value.transform);

            BoxCollider collider = obj.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = new Vector3(value.sprites[0].bounds.size.x,value.sprites[0].bounds.size.y,0.1f);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.AddComponent<ProximityTrigger>();

            obj.transform.SetParent(value.transform);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Add collision"))
        {
            AlignSprites value = (AlignSprites)target;

            GameObject obj = new GameObject("Collision");
            obj.transform.SetParent(value.transform);

            BoxCollider collider = obj.AddComponent<BoxCollider>();
            collider.isTrigger = false;
            collider.size = new Vector3(value.sprites[0].bounds.size.x,value.sprites[0].bounds.size.y,0.1f);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.AddComponent<CollisionTrigger>();
        }

        obj.ApplyModifiedProperties();
    }

    private void RotateInstantiatedSprites()
    {
        AlignSprites value = (AlignSprites)target;

        int current = 0;
        while(current < value.transform.childCount){
            if (value.transform.GetChild(current).GetComponent<SpriteRenderer>())
            {
                value.transform.GetChild(current).transform.localRotation = Quaternion.Euler(value.rotation,0,0);
            }
            current++;
        }
    }


    private void DestroyInstantiatedSprites()
    {
        AlignSprites value = (AlignSprites)target;

        int current = 0;
        while(current < value.transform.childCount){
            if (value.transform.GetChild(current).GetComponent<SpriteRenderer>())
            {
                DestroyImmediate(value.transform.GetChild(current).gameObject);
            }
            else
            {
                current++;
            }
        }
    }
}
