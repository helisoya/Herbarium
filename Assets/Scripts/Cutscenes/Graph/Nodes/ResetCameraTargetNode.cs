using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node that resets the camera target
/// </summary>
[CreateNodeMenu("Event/ResetCameraTarget")]
public class ResetCameraTargetNode : HerbariumNode
{

    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {   
        Player.instance.ResetCameraTarget();
        
        yield return 0;
    }
}