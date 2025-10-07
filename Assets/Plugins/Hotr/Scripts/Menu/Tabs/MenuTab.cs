using UnityEngine;

namespace MyHerbagnole
{
    public class MenuTab : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameObject root;

        public bool active
        {
            get
            {
                return root.activeInHierarchy;
            }
        }

        public virtual void OnOpen()
        {
            root.SetActive(true);
        }


        public virtual void OnClose()
        {
            root.SetActive(false);
        }

        public virtual void OnMove(int playerId, Vector2 playerMovement)
        {

        }

        public virtual void OnSubmit(int playerId)
        {

        }

        public virtual void OnAddPlayer(int playerId, BagnolePlayer player)
        {

        }

        public virtual void OnRemovePlayer(int playerId, BagnolePlayer player)
        {

        }
    }
}
