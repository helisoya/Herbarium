using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node that can close the dialog window
/// </summary>
[CreateNodeMenu("Event/Close Dialog window")]
public class CloseDialogWindowNode : HerbariumNode
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
        GameGUI.instance.SetDialogOpen(false);

        yield return 0;
    }
}