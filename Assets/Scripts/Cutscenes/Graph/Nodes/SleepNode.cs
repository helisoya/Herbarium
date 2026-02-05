using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node in a cutscene graph
/// </summary>
[CreateNodeMenu("Event/Sleep")]
public class SleepNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private bool refreshRegrowthSystem = true;
    [SerializeField] private float sleepTime = 1f;
    [Output(connectionType = ConnectionType.Override)] public bool exit;
    

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        AudioManager.Instance.PlayEvent2D(EventID.MusGoodNight);

        GameGUI.instance.ShowPopup("Popup_Sleep",null);

        yield return new WaitForSeconds(1.0f);

        GameGUI.instance.FadeTo(1);
        yield return new WaitForEndOfFrame();
        while(GameGUI.instance.fading)
        {
            yield return new WaitForEndOfFrame();
        }

        GameGUI.instance.HidePopup();

        if (refreshRegrowthSystem)
        {
            GameManager.instance.GetPlayerDataHandler().IncrementRegrowthSystem();
            Map.instance.TriggerOnRegrowthSystemRefresh();
        }

        GameManager.instance.GetPlayerDataHandler().SaveData();

        yield return new WaitForSeconds(sleepTime);

        GameGUI.instance.FadeTo(0);
        yield return new WaitForEndOfFrame();
        while(GameGUI.instance.fading)
        {
            yield return new WaitForEndOfFrame();
        }

        AudioManager.Instance.PlayEvent2D(EventID.MusGoodMorning);

        yield return 0;
    }
}
