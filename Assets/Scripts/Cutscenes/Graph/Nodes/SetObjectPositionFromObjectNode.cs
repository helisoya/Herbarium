using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that can set an object's position using another object's position
/// </summary>
[CreateNodeMenu("Event/SetObjectPositionFromObject")]
public class SetObjectPositionFromObjectNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string objectID;
    [SerializeField] private string targetID;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameObject target = CutsceneManager.instance.GetObject(targetID);
        GameObject obj = CutsceneManager.instance.GetObject(objectID);
        if(obj && target) obj.transform.position = target.transform.position;
        yield return 0;
    }
}
