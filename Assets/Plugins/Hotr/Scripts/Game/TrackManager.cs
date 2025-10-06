using UnityEngine;
using UnityEngine.InputSystem;

namespace MyHerbagnole
{
    /// <summary>
    /// Handles the game's status in My Herbagnole
    /// </summary>
    public class TrackManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnsRoot;
        [SerializeField] private int lapsAmount = 3;

        private string[] currentSchemes = { "Keyboard&Mouse", "KeyboardAlt" };
        private PlayerData[] playerDatas;

        void Start()
        {
            playerDatas = new PlayerData[currentSchemes.Length];
            for (int i = 0; i < currentSchemes.Length; i++)
            {
                playerDatas[i] = new PlayerData();
                playerDatas[i].lap = 0;
                playerDatas[i].input = PlayerInput.Instantiate(playerPrefab, controlScheme: currentSchemes[i], pairWithDevice: Keyboard.current);
                playerDatas[i].controller = playerDatas[i].input.GetComponent<CarController>();
                playerDatas[i].controller.Init(i, this);
                playerDatas[i].controller.transform.position = playerSpawnsRoot.GetChild(i).position;

                playerDatas[i].controller.SetCanMove(true);
            }
        }

        public void DoLap(int id)
        {
            playerDatas[id].lap++;
            if (playerDatas[id].lap >= lapsAmount)
            {
                foreach (PlayerData data in playerDatas)
                {
                    data.controller.SetCanMove(false);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public struct PlayerData {
            public PlayerInput input;
            public int lap;
            public CarController controller;
        }
    }
}

