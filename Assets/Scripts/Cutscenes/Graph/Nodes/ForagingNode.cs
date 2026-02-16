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
    [Output(connectionType = ConnectionType.Override)] public bool exitCancel;
    

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        if (GameManager.instance.GetPlayerDataHandler().GetRemainingInventorySpace() == 0)
        {
            GameGUI.instance.ShowPopup("Popup_Foraging_Full",null);
            yield return 2;
        }
        else
        {
            GameGUI.instance.FadeTo(1);
            yield return new WaitForEndOfFrame();
            while (GameGUI.instance.fading)
            {
                yield return new WaitForEndOfFrame();
            }

            MicroInteraction.EndingType result;
            int resultIdx = 0;

            if (Settings.instance.IsAutocompleteInteractionEnabled())
            {
                result = MicroInteraction.EndingType.SUCCESS;
            }
            else
            {
                Player.instance.StartMicroInteraction("SC_Foraging",plantId);

                yield return new WaitForEndOfFrame();
                while (Player.instance.inMicroInteraction)
                {
                    yield return new WaitForEndOfFrame();
                }

                result = Player.instance.lastMicroInteractionEnding;
            }

            GameObject obj = CutsceneManager.instance.GetObject("THIS");

            switch (result)
            {
                case MicroInteraction.EndingType.SUCCESS:
                    if(obj) obj.SendMessage("TagEntityAsNotRegrown");
                    GameManager.instance.GetPlayerDataHandler().AddInInventory(plantId);
                    GameGUI.instance.ShowPopup("Popup_Foraging_Done",new object[]{Locals.GetLocal(Plant.GetName(plantId))});
                    resultIdx = 0;
                    break;

                case MicroInteraction.EndingType.FAILURE:
                    if(obj) obj.SendMessage("TagEntityAsNotRegrown");
                    GameGUI.instance.ShowPopup("Popup_Foraging_Fail",null);
                    resultIdx = 1;
                    break;

                case MicroInteraction.EndingType.CANCEL:
                    resultIdx = 3;
                    break;
            }

            GameGUI.instance.FadeTo(0);

            yield return resultIdx;
        }
    }
}
