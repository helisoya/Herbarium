using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using XNodeEditor;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

/// <summary>
/// Editor for aligning sprites
/// </summary>
[CustomEditor(typeof(ForagingPlantSetup))]
public class ForagingPlantSetupEditor : Editor
{
    SerializedObject obj;
    SerializedProperty value;

    private void Init()
    {
        obj = new SerializedObject(target);
        value = obj.FindProperty("value");
    }

    public override void OnInspectorGUI(){
        if(obj == null) Init();

        EditorGUILayout.PropertyField(value);
        EditorGUILayout.Space();

        if (GUILayout.Button("Set to Hinge Joints (angles)"))
        {
            HingeJoint2D[] joints = ((ForagingPlantSetup)target).GetComponentsInChildren<HingeJoint2D>();
            foreach(HingeJoint2D joint in joints)
            {
                joint.limits = new JointAngleLimits2D(){min = -value.floatValue, max = value.floatValue};
            }
        }

        obj.ApplyModifiedProperties();
    }
}
