using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that sets the interactable graph
/// </summary>
[CreateNodeMenu("Event/Set Interactable Graph")]
public class SetInteractableGraphNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string objectID;
    [SerializeField] private DialogGraph nextGraph;
    [SerializeField] private MusicManager.CutSceneID audioCutSceneID;
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
            obj.SendMessage("SetDialogGraph",nextGraph,SendMessageOptions.DontRequireReceiver);
            obj.SendMessage("SetAudioCutsceneId",audioCutSceneID,SendMessageOptions.DontRequireReceiver);
        }

        yield return 0;
    }
}
