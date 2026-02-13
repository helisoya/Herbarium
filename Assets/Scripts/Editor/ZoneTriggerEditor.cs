using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor for the zone trigger
/// </summary>
[CustomEditor(typeof(ZoneTrigger))]
public class ZoneTriggerEditor : Editor
{
    SerializedObject obj;
    SerializedProperty dotAngle;

    private void Init()
    {
        obj = new SerializedObject(target);
        dotAngle = obj.FindProperty("dotAngle");
    }

    public override void OnInspectorGUI(){
        if(obj == null) Init();

        EditorGUILayout.Space();

        base.OnInspectorGUI();

        if (GUILayout.Button("Set Perpendicular"))
        {
            dotAngle.vector3Value = ((ZoneTrigger)target).transform.forward;
        }

        obj.ApplyModifiedProperties();
    }
}
