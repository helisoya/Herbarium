using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyHerbagnole{
    public class LevelSelectionTab : MenuTab
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Image icon;
        [SerializeField] private float moveCooldown = 0.5f;

        private int playerChoice;
        private float playerMove;
        private float currentMoveCooldown;

        private BagnoleManager.RaceData[] raceData;

        public override void OnOpen()
        {
            base.OnOpen();

            playerChoice = 0;
            playerMove = 0.0f;
            currentMoveCooldown = 0.0f;
            raceData = BagnoleManager.instance.races;

            IncrementPlayer(0);
        }


        void Update()
        {
            if (!active) return;

            if (currentMoveCooldown > 0)
            {
                currentMoveCooldown -= Time.deltaTime;
                return;
            }

            currentMoveCooldown = moveCooldown;

            if (Mathf.Abs(playerMove) >= 0.5f && playerMove != 0.0f)
            {
                IncrementPlayer(playerMove > 0 ? 1 : -1);
            }
        }

        void IncrementPlayer(int direction)
        {
            playerChoice = (playerChoice + direction + raceData.Length) % raceData.Length;
            label.text = raceData[playerChoice].label;
            icon.sprite = raceData[playerChoice].sprite;
        }

        public override void OnClose()
        {
            base.OnClose();
        }

        public override void OnMove(int playerId, Vector2 playerMovement)
        {
            if (playerId != 0) return;

            playerMove = playerMovement.x;
            if (playerMovement.x != 0 && Mathf.Abs(playerMovement.x) > 0.95f)
            {
                currentMoveCooldown = moveCooldown;
                IncrementPlayer(playerMovement.x > 0 ? 1 : -1);
            }
        }

        public override void OnSubmit(int playerId)
        {
            if(playerId == 0) BagnoleManager.instance.GoToRace(raceData[playerChoice].raceName);
        }

        public override void OnAddPlayer(int playerId, BagnolePlayer player)
        {
        }

        public override void OnRemovePlayer(int playerId, BagnolePlayer player)
        {
        }
    }
}

