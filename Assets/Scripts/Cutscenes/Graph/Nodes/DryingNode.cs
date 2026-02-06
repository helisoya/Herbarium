using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node in a cutscene graph
/// </summary>
[CreateNodeMenu("Event/Drying")]
public class DryingNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private bool showHerbariumImmediatly = true;
    [Output(connectionType = ConnectionType.Override)] public bool exitSuccess;
    [Output(connectionType = ConnectionType.Override)] public bool exitAlreadyDone;
    [Output(connectionType = ConnectionType.Override)] public bool exitCancel;
    

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        
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

            // Temp fix for herbarium & secret plants
            // Nothing to see here, I swear
            bool isSecret = false;
            string[] secretPlants = GameManager.instance.GetPlantDatabase().GetExistingSecretPlants();
            foreach(string secret in secretPlants)
            {
                if (selectedPlant.Equals(secret))
                {
                    isSecret = true;
                    break;
                }
            }

            if (isSecret || GameManager.instance.GetPlayerDataHandler().IsUnlockedInHerbarium(selectedPlant))
            {
                GameGUI.instance.ShowPopup("Popup_Drying_Fail",null);
                
                yield return new WaitForSeconds(2.0f);

                yield return 1;
            }
            else
            {

                GameGUI.instance.ShowPopup("Popup_Drying_Good",null);

                AudioManager.Instance.PlayEvent2D(EventID.DryPlant);

                yield return new WaitForSeconds(2.0f);

                GameManager.instance.GetPlayerDataHandler().RemoveFromInventoryAt(selected);
                GameManager.instance.GetPlayerDataHandler().AddHerbariumPage(selectedPlant);

                if (showHerbariumImmediatly)
                {
                    GameGUI.instance.HidePopup();
                    GameGUI.instance.OpenHerbarium();
                    GameGUI.instance.HerbariumShowPlantPage(GameManager.instance.GetPlantDatabase().PlantIDToIndex(selectedPlant));
                    yield return new WaitForEndOfFrame();
                    while(GameGUI.instance.inHerbarium)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }

                yield return 0;
            }
        }
    }
}
