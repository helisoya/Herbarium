using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that can move an object to another object
/// </summary>
[CreateNodeMenu("Event/MoveObjectToObject")]
public class MoveObjectToObjectNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string objectID;
    [SerializeField] private string targetID;
    [SerializeField] private float speed = 2.5f;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameObject obj = CutsceneManager.instance.GetObject(objectID);
        GameObject target = CutsceneManager.instance.GetObject(targetID);
        if (obj && target)
        {
            while(obj.transform.position != target.transform.position)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position,target.transform.position,speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        yield return 0;
    }
}
