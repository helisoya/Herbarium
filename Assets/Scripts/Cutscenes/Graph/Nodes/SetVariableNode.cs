using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node that can set a variable
/// </summary>
[CreateNodeMenu("Event/SetVariable")]
public class SetVariableNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string variableID;
    [SerializeField] private SetType type;
    [SerializeField] private int value;
    [Output(connectionType = ConnectionType.Override)] public bool exit;


    public enum SetType
    {
        SET,
        ADD
    }

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        int newValue = GameManager.instance.GetPlayerDataHandler().GetVariable(variableID);

        switch (type)
        {
            case SetType.SET:
                newValue = value;
                break;
            case SetType.ADD:
                newValue += value;
                break;
        }

        GameManager.instance.GetPlayerDataHandler().SetVariable(variableID,newValue);
    
        yield return 0;
    }
}
