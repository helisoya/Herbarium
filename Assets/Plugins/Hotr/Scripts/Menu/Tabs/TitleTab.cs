using UnityEngine;


namespace MyHerbagnole
{
    public class TitleMenu : MenuTab
    {
        public override void OnSubmit(int playerId)
        {
            BagnoleMenuManager.instance.SetActiveTab(1);
        }
    }
}
