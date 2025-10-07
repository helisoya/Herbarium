using UnityEditor;
using UnityEngine;

namespace MyHerbagnole
{
    /// <summary>
    /// Handles the main menu in My Herbagnole
    /// </summary>
    public class BagnoleMenuManager : MonoBehaviour
    {
        public static BagnoleMenuManager instance;


        [Header("Tabs")]
        [SerializeField] private MenuTab[] tabs;

        private int activeTab;

        void Awake()
        {
            instance = this;
            activeTab = 0;
            SetActiveTab(0);
        }

        public void SetActiveTab(int idx)
        {
            activeTab = idx;
            for (int i = 0; i < tabs.Length; i++)
            {
                if (i == idx) tabs[i].OnOpen();
                else tabs[i].OnClose();
            }
        }

        public void ForwardOnMove(int id, Vector2 input)
        {
            tabs[activeTab].OnMove(id, input);
        }

        public void ForwardOnSubmit(int id)
        {
            tabs[activeTab].OnSubmit(id);
        }

        public void ForwardAddPlayer(int id, BagnolePlayer player)
        {
            tabs[activeTab].OnAddPlayer(id,player);
        }

        public void ForwardRemovePlayer(int id, BagnolePlayer player)
        {
            tabs[activeTab].OnRemovePlayer(id,player);
        }
    }
}

