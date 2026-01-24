using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that checks if a plant is in the inventory or not
/// </summary>
[CreateNodeMenu("Control/Is Plant In Inventory")]
public class HasPlantInInventoryNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string plantID;
    [Output(connectionType = ConnectionType.Override)] public bool exitPlantInInventory;
    [Output(connectionType = ConnectionType.Override)] public bool exitPlantNotInInventory;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        bool checkingForAny = string.IsNullOrEmpty(plantID);
        bool atLeastOne = false;
        bool skip = false;
        string item;
        PlayerDataHandler handler = GameManager.instance.GetPlayerDataHandler();
        for(int i = 0; i < handler.GetInventorySize(); i++)
        {
            item = handler.GetInventoryItem(i);
            if(checkingForAny && item != null)
            {
                atLeastOne = true;
            }
            else if (!checkingForAny)
            {
                if (item.Equals(plantID))
                {
                    skip = true;
                    yield return 0;
                    break; 
                }
            }
        }

        if(checkingForAny && atLeastOne)
        {
            yield return 0;
        }
        else if(!skip)
        {
            yield return 1;
        }
        
    }
}
