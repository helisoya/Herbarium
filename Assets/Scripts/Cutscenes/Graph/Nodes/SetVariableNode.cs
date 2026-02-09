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
        PlayerDataHandler handler = GameManager.instance.GetPlayerDataHandler();
        int newValue = handler.GetVariable(variableID);

        switch (type)
        {
            case SetType.SET:
                newValue = value;
                break;
            case SetType.ADD:
                newValue += value;
                break;
        }

        handler.SetVariable(variableID,newValue);

        Quest quest = handler.GetQuestFromVariable(variableID);
        if(quest != null && handler.IsPinned(quest.id))
        {
            if(newValue == -1 || newValue == 100)
            {
                handler.UnpinQuest(quest.id);
                GameGUI.instance.RemovePin(quest.id);
            }
            else
            {
                GameGUI.instance.RefreshQuestPin(quest.id);
            }
        }else if(quest != null && newValue == 0)
        {
            // Quest not pinned and started
            handler.PinQuest(quest.id);
            GameGUI.instance.AddPin(quest.id);
        }
    
        yield return 0;
    }
}
