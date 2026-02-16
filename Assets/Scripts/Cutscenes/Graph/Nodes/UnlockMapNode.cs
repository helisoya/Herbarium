using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node that can unlock the map
/// </summary>
[CreateNodeMenu("Event/Unlock Map")]
public class UnlockMapNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private bool mapIsNowUnlocked;
    [SerializeField] private bool showMapIfUnlocked;
    [Output(connectionType = ConnectionType.Override)] public bool exit;
    

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {

        GameManager.instance.GetPlayerDataHandler().UnlockMap(mapIsNowUnlocked);

        if(mapIsNowUnlocked && showMapIfUnlocked)
        {
            GameGUI.instance.OpenMap();
            yield return new WaitForEndOfFrame();
            while (GameGUI.instance.mapOpen)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        yield return 0;
    }
}
