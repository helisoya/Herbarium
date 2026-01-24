using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that checks if the inventory is full
/// </summary>
[CreateNodeMenu("Control/Is Inventory Full")]
public class IsInventoryFullNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [Output(connectionType = ConnectionType.Override)] public bool exitInventoryFull;
    [Output(connectionType = ConnectionType.Override)] public bool exitInventoryNotFull;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        yield return GameManager.instance.GetPlayerDataHandler().GetRemainingInventorySpace() == 0 ? 0 : 1;        
    }
}
