using UnityEngine;
using UnityEngine.InputSystem;

namespace MyHerbagnole
{
    /// <summary>
    /// Handles the game's status in My Herbagnole
    /// </summary>
    public class TrackManager : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnsRoot;
        [SerializeField] private int lapsAmount = 3;
        private PlayerData[] playerDatas;

        void Start()
        {
            playerDatas = new PlayerData[4];
            for (int i = 0; i < 4; i++)
            {
                if (!BagnoleManager.instance.players[i]) continue;

                playerDatas[i] = new PlayerData();
                playerDatas[i].lap = 0;
                playerDatas[i].player = BagnoleManager.instance.players[i];
                playerDatas[i].controller = playerDatas[i].player.CreateCar();
                playerDatas[i].controller.Init(i, this);
                playerDatas[i].controller.transform.position = playerSpawnsRoot.GetChild(i).position;

                playerDatas[i].controller.SetCanMove(true);
            }
            BagnoleManager.instance.EnableSplitScreen(true);
        }

        public void DoLap(int id)
        {
            playerDatas[id].lap++;
            if (playerDatas[id].lap >= lapsAmount)
            {
                foreach (PlayerData data in playerDatas)
                {
                    if(data.controller) data.controller.SetCanMove(false);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public struct PlayerData {
            public BagnolePlayer player;
            public int lap;
            public CarController controller;
        }
    }
}

