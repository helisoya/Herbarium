using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a dialog node
/// </summary>
[CreateNodeMenu("Event/Dialog")]
public class DialogNode : HerbariumNode {

    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string dialogID;
    [SerializeField] private string nameID;
    [SerializeField] private string titleID;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameManager.instance.GetPlayerDataHandler().AddDialogLog(dialogID,nameID);
        GameGUI.instance.ShowDialog(dialogID,nameID,titleID);

        // Dialog appears
        while(GameGUI.instance.showingDialog){
            if(CutsceneManager.instance.ConsumeUserSubmit()) GameGUI.instance.SetSkipDialogTag();
            yield return new WaitForEndOfFrame();
        }

        // Wait for user input
        while(!CutsceneManager.instance.ConsumeUserSubmit()){
            yield return new WaitForEndOfFrame();
        }
        GameGUI.instance.SetDialogOpen(false);
        
        yield return 0;
    }
}