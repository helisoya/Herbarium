using TMPro;
using UnityEngine;

namespace MyHerbagnole
{
    public class KartSelectionTab : MenuTab
    {
        [SerializeField] private Transform[] spots;
        [SerializeField] private TextMeshPro[] labels;
        [SerializeField] private float moveCooldown = 0.25f;

        private bool[] locked;
        private int[] playerChoices;
        private float[] playerMoves;
        private GameObject[] driverVisuals;
        private CarData[] kartVisuals;
        private float[] currentMoveCooldown;

        private BagnoleManager.CarData[] carData;

        public override void OnOpen()
        {
            base.OnOpen();

            locked = new bool[4];
            playerChoices = new int[4];
            driverVisuals = new GameObject[4];
            kartVisuals = new CarData[4];
            playerMoves = new float[4];
            currentMoveCooldown = new float[4];
            carData = BagnoleManager.instance.cars;

            for (int i = 0; i < 4; i++)
            {
                locked[i] = !BagnoleManager.instance.players[i];
                if (!locked[i])
                {
                    driverVisuals[i] = Instantiate(BagnoleManager.instance.players[i].prefabDriver);
                    IncrementPlayer(i, 0);
                } 
            }
        }

        void AddKart(int playerId, GameObject prefab, Transform spot) {

            CarData newCar = Instantiate(prefab, spot).GetComponent<CarData>();
            newCar.transform.localPosition = new Vector3(0, 0, -2.5f);
            driverVisuals[playerId].transform.SetParent(newCar.driverSpot);
            driverVisuals[playerId].transform.localPosition = Vector3.zero;
            driverVisuals[playerId].GetComponent<Animator>().runtimeAnimatorController = newCar.driverController;

            if (kartVisuals[playerId]) Destroy(kartVisuals[playerId].gameObject);
            kartVisuals[playerId] = newCar;
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

                if (!locked[i] && Mathf.Abs(playerMoves[i]) >= 0.5f  && playerMoves[i] != 0.0f)
                {
                    IncrementPlayer(i, playerMoves[i] > 0 ? 1 : -1);
                }
            }
        }

        void IncrementPlayer(int playerId, int direction)
        {
            playerChoices[playerId] = (playerChoices[playerId] + direction + carData.Length) % carData.Length;
            AddKart(playerId, carData[playerChoices[playerId]].visualPrefab, spots[playerId]);
            labels[playerId].text = carData[playerChoices[playerId]].label;
        }

        public override void OnClose()
        {
            base.OnClose();

            if (driverVisuals == null) return;

            for (int i = 0; i < 4; i++)
            {
                labels[i].text = "";
                labels[i].color = Color.white;
                if (kartVisuals[i]) Destroy(kartVisuals[i].gameObject);
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
            labels[playerId].color = Color.green;

            foreach (bool playerLocked in locked)
            {
                if (!playerLocked) return;
            }

            BagnoleManager.instance.ApplyCars(playerChoices);
            BagnoleMenuManager.instance.SetActiveTab(3);
        }

        public override void OnAddPlayer(int playerId, BagnolePlayer player)
        {
            locked[playerId] = false;
            playerChoices[playerId] = 0;
            driverVisuals[playerId] = Instantiate(BagnoleManager.instance.players[playerId].prefabDriver);
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

