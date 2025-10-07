using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace MyHerbagnole
{
    /// <summary>
    /// Represents the player manager in my Herbagnole
    /// </summary>
    public class BagnoleManager : MonoBehaviour
    {

        public enum GameState
        {
            MAINMENU,
            RACE
        }

        [Serializable]
        public struct DriverData {
            public GameObject prefab;
            public string label;
        }

        [Serializable]
        public struct CarData {
            public GameObject visualPrefab;
            public GameObject playablePrefab;
            public string label;
        }

        [Serializable]
        public struct RaceData {
            public Sprite sprite;
            public string label;
            public string raceName;
        }


        [Header("Players")]
        [SerializeField] private PlayerInputManager inputManager;

        [Header("Data")]
        public CarData[] cars;
        public DriverData[] drivers;
        public RaceData[] races;
        public string mainMenuName;


        [Header("Debug")]
        [SerializeField] private bool debug;
        [SerializeField] private bool startInRace;
        [SerializeField] private string[] debugSchemes = { "Keyboard&Mouse", "KeyboardAlt" };

        public static BagnoleManager instance;
        public GameState state { get; private set; }
        private bool canAddPlayers = true;
        public BagnolePlayer[] players { get; private set; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                EnableSplitScreen(false);
                players = new BagnolePlayer[4];
                SetCanAddPlayer(true);

                if (debug)
                {
                    for (int i = 0; i < debugSchemes.Length; i++)
                    {
                        PlayerInput.Instantiate(inputManager.playerPrefab, controlScheme: debugSchemes[i], pairWithDevice: Keyboard.current);
                    }

                    if(startInRace) state = GameState.RACE;
                }

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void EnableSplitScreen(bool value)
        {
            inputManager.splitScreen = value;
        }

        public void SetCanAddPlayer(bool value)
        {
            if (canAddPlayers != value)
            {
                canAddPlayers = value;
                if (value) inputManager.EnableJoining();
                else inputManager.DisableJoining();
            }
        }

        public void OnPlayerJoined(PlayerInput input)
        {
            input.transform.SetParent(transform);
            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i])
                {
                    input.GetComponent<BagnolePlayer>().Init(i, input);
                    players[i] = input.GetComponent<BagnolePlayer>();
                    players[i].SetPrefabs(
                        cars[UnityEngine.Random.Range(0, cars.Length)].playablePrefab,
                        drivers[UnityEngine.Random.Range(0, drivers.Length)].prefab);

                    if (state == GameState.MAINMENU) BagnoleMenuManager.instance.ForwardAddPlayer(i, players[i]);
                    break;
                }
            }
        }

        public void OnPlayerLeft(PlayerInput input)
        {
            BagnolePlayer player = input.GetComponent<BagnolePlayer>();
            if (state == GameState.MAINMENU) BagnoleMenuManager.instance.ForwardAddPlayer(player.id, player);
            players[player.id] = null;
        }


        public void ApplyDrivers(int[] choices)
        {
            for (int i = 0; i < 4; i++)
            {
                if (players[i]) players[i].SetDriver(drivers[choices[i]].prefab);
            }
        }

        public void ApplyCars(int[] choices)
        {
            for (int i = 0; i < 4; i++)
            {
                if (players[i]) players[i].SetCar(cars[choices[i]].playablePrefab);
            }
        }


        public void GoToMainMenu()
        {
            EnableSplitScreen(false);
            foreach (BagnolePlayer player in players)
            {
                if (player) player.DestroyCar();
            }
            SceneManager.LoadScene(mainMenuName);
            SetCanAddPlayer(true);
            state = GameState.MAINMENU;
        }

        public void GoToRace(string raceName)
        {
            SetCanAddPlayer(false);
            SceneManager.LoadScene(raceName);
            state = GameState.RACE;
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

