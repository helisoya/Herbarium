using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that can set an object's position
/// </summary>
[CreateNodeMenu("Event/SetObjectPosition")]
public class SetObjectPositionNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string objectID;
    [SerializeField] private Vector3 absolutePositionInWorld;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameObject obj = CutsceneManager.instance.GetObject(objectID);
        if(obj) obj.transform.position = absolutePositionInWorld;
        yield return 0;
    }
}
