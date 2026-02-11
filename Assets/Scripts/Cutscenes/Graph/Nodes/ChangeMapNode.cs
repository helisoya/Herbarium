using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a node that can change the map
/// </summary>
[CreateNodeMenu("Event/Change Map")]
public class ChangeMapNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string mapID;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameGUI.instance.FadeTo(1);
        yield return new WaitForEndOfFrame();
        while (GameGUI.instance.fading)
        {
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(mapID);
        
        yield return 0;
    }
}
