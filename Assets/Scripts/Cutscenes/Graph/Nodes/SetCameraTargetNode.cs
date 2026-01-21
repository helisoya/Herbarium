using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node that sets the camera target
/// </summary>
[CreateNodeMenu("Event/SetCameraTarget")]
public class SetCameraTargetNode : HerbariumNode
{

    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string targetID;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {   
        GameObject obj = CutsceneManager.instance.GetObject(targetID);
        if(obj) Player.instance.SetCameraTarget(obj.transform);
        
        yield return 0;
    }
}