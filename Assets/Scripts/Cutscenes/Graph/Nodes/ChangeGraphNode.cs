using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that changes the current dialog graph
/// </summary>
[CreateNodeMenu("Event/Change Graph")]
public class ChangeGraphNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private DialogGraph nextGraph;
    [SerializeField] private MusicManager.CutSceneID audioCutSceneID;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        CutsceneManager.instance.ProcessCutscene(nextGraph,audioCutSceneID,null);
        yield return 0;
    }
}
