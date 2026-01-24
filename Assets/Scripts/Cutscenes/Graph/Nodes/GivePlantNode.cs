using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that allows the player to give a plant
/// </summary>
[CreateNodeMenu("Event/Give Plant")]
public class GivePlantNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string plantID;
    [SerializeField] private bool removePlantIfPossible = true;
    [Output(connectionType = ConnectionType.Override)] public bool exitSuccess;
    [Output(connectionType = ConnectionType.Override)] public bool exitPlantNotCorrect;
    [Output(connectionType = ConnectionType.Override)] public bool exitCancel;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        bool anyPlantWorks = string.IsNullOrEmpty(plantID);


        GameGUI.instance.OpenInventoryGive();
        yield return new WaitForEndOfFrame();
        while(GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED)
        {
            yield return new WaitForEndOfFrame();
        }

        // -1 will be Close, 0-2 will be actual items
        int selected = GameGUI.instance.selectedRadialIndex - 1;

        if(selected == -1)
        {
            yield return 2;
        }
        else
        {
            string selectedPlant = GameManager.instance.GetPlayerDataHandler().GetInventoryItem(selected);

            if (!anyPlantWorks && !selectedPlant.Equals(plantID))
            {
                yield return 1;
            }
            else
            {
                if (removePlantIfPossible)
                {
                    GameManager.instance.GetPlayerDataHandler().RemoveFromInventoryAt(selected);
                }
                yield return 0;
            }
        }
    }
}
