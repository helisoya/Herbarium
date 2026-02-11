using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node that triggers a specific music
/// </summary>
[CreateNodeMenu("Event/Trigger Music")]
public class MusicNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private MusicManager.CutSceneID cutSceneID;
    [SerializeField] private bool triggerStart = true;
    [Output(connectionType = ConnectionType.Override)] public bool exit;
    

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        if(triggerStart)MusicManager.Instance.PostCutScene(cutSceneID);
        else MusicManager.Instance.EndCutScene(cutSceneID);

        yield return 0;
    }
}
