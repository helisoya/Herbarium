using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that can show a popup
/// </summary>
[CreateNodeMenu("Event/Show Popup")]
public class ShowPopupNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string popupKey;
    [SerializeField] private bool waitForEnd;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameGUI.instance.ShowPopup(popupKey,null);
        if (waitForEnd)
        {
            yield return new WaitForEndOfFrame();
            while (GameGUI.instance.showingPopup)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        yield return 0;
    }
}
