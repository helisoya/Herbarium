using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node in a cutscene graph
/// </summary>
[CreateNodeMenu("Event/Foraging")]
public class ForagingNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string plantId;
    [Output(connectionType = ConnectionType.Override)] public bool exitSuccess;
    [Output(connectionType = ConnectionType.Override)] public bool exitFailure;
    [Output(connectionType = ConnectionType.Override)] public bool exitNoSpaceInInventory;
    

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        if (GameManager.instance.GetPlayerDataHandler().GetRemainingInventorySpace() == 0)
        {
            yield return 2;
        }
        else
        {
            if (Settings.instance.IsAutocompleteInteractionEnabled())
            {
                CutsceneManager.instance.SetObjectActive("THIS",false);
                GameManager.instance.GetPlayerDataHandler().AddInInventory(plantId);
                yield return 0;
            }
            else
            {
                Player.instance.StartMicroInteraction("SC_Foraging",plantId);

                yield return new WaitForEndOfFrame();
                while (Player.instance.inMicroInteraction)
                {
                    yield return new WaitForEndOfFrame();
                }

                if (Player.instance.lastMicroInteractionSucceeded)
                {
                    CutsceneManager.instance.SetObjectActive("THIS",false);
                    GameManager.instance.GetPlayerDataHandler().AddInInventory(plantId);
                    yield return 0;
                }
                else
                {
                    yield return 1;
                }
            }
        }
    }
}
