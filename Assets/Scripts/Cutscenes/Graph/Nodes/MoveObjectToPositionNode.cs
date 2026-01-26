using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that can move an object to a set position
/// </summary>
[CreateNodeMenu("Event/MoveObjectToPosition")]
public class MoveObjectToPositionNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string objectID;
    [SerializeField] private Vector3 absolutePositionInWorld;
    [SerializeField] private float speed = 2.5f;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameObject obj = CutsceneManager.instance.GetObject(objectID);
        if (obj)
        {
            while(obj.transform.position != absolutePositionInWorld)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position,absolutePositionInWorld,speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        yield return 0;
    }
}
