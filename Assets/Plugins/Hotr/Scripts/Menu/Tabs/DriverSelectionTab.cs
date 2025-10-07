using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace MyHerbagnole
{
    public class DriverSelectionMenu : MenuTab
    {
        [SerializeField] private Transform[] spots;
        [SerializeField] private TextMeshPro[] labels;
        [SerializeField] private AnimatorOverrideController characterController;
        [SerializeField] private float moveCooldown = 0.25f;

        private bool[] locked;
        private int[] playerChoices;
        private float[] playerMoves;
        private GameObject[] driverVisuals;
        private float[] currentMoveCooldown;

        private BagnoleManager.DriverData[] driverData;

        public override void OnOpen()
        {
            base.OnOpen();

            locked = new bool[4];
            playerChoices = new int[4];
            driverVisuals = new GameObject[4];
            playerMoves = new float[4];
            currentMoveCooldown = new float[4];
            driverData = BagnoleManager.instance.drivers;

            for (int i = 0; i < 4; i++)
            {
                locked[i] = !BagnoleManager.instance.players[i];
                if(!locked[i]) IncrementPlayer(i, 0);
            }
        }

        void AddDriver(int playerId, GameObject prefab, Transform spot) {
            if (driverVisuals[playerId]) Destroy(driverVisuals[playerId]);
            driverVisuals[playerId] = Instantiate(prefab, spot);
            driverVisuals[playerId].GetComponent<Animator>().runtimeAnimatorController = characterController;
        }

        void Update()
        {
            if (!active) return;

            for (int i = 0; i < 4; i++)
            {
                if (currentMoveCooldown[i] > 0)
                {
                    currentMoveCooldown[i] -= Time.deltaTime;
                    continue;
                }

                currentMoveCooldown[i] = moveCooldown;

                if (!locked[i] && Mathf.Abs(playerMoves[i]) >= 0.5f && playerMoves[i] != 0.0f)
                {
                    IncrementPlayer(i, playerMoves[i] > 0 ? 1 : -1);
                }
            }
        }

        void IncrementPlayer(int playerId, int direction)
        {
            playerChoices[playerId] = (playerChoices[playerId] + direction + driverData.Length) % driverData.Length;
            AddDriver(playerId, driverData[playerChoices[playerId]].prefab, spots[playerId]);
            labels[playerId].text = driverData[playerChoices[playerId]].label;
        }

        public override void OnClose()
        {
            base.OnClose();

            if (driverVisuals == null) return;

            for (int i = 0; i < 4; i++)
            {
                labels[i].text = "";
                labels[i].color = Color.white;
                if (driverVisuals[i]) Destroy(driverVisuals[i]);
            }
        }

        public override void OnMove(int playerId, Vector2 playerMovement)
        {
            if (locked[playerId]) return;

            playerMoves[playerId] = playerMovement.x;
            if (playerMovement.x != 0 && Mathf.Abs(playerMovement.x) > 0.95f)
            {
                currentMoveCooldown[playerId] = moveCooldown;
                IncrementPlayer(playerId, playerMovement.x > 0 ? 1 : -1);
            }
        }

        public override void OnSubmit(int playerId)
        {
            locked[playerId] = true;
            playerMoves[playerId] = 0;
            labels[playerId].color = Color.green;

            foreach (bool playerLocked in locked)
            {
                if (!playerLocked) return;
            }

            BagnoleManager.instance.ApplyDrivers(playerChoices);
            BagnoleMenuManager.instance.SetActiveTab(2);
        }

        public override void OnAddPlayer(int playerId, BagnolePlayer player)
        {
            locked[playerId] = false;
            playerChoices[playerId] = 0;
            IncrementPlayer(playerId, 0);
        }

        public override void OnRemovePlayer(int playerId, BagnolePlayer player)
        {
            playerMoves[playerId] = 0;
            if (driverVisuals[playerId]) Destroy(driverVisuals[playerId]);
            labels[playerId].text = "";
            OnSubmit(playerId);
        }

    }
}