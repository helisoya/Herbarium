using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the game's flow
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerDataHandler playerDataHandler;
    [SerializeField] private PlantDatabase plantDatabase;
    [SerializeField] private AudioManager audioManager;
    public static GameManager instance;

    public bool inMainMenu {get; set;}
    public bool loadingSave {get; set;}

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            Instantiate(audioManager, transform);

            Locals.Init();
            Settings.Init();
            playerDataHandler.ResetData();
            plantDatabase.Init();

            DontDestroyOnLoad(gameObject);


            // Debug build
            if (Debug.isDebugBuild)
            {
                //playerDataHandler.AddInInventory("TestPlant1");
                //playerDataHandler.AddHerbariumPage("TestPlant1");
                playerDataHandler.AddHerbariumPage("TestPlant2");
                playerDataHandler.SetVariable("quest_test",0);
            }
        }
    }

    /// <summary>
	/// Gets the player data handler
	/// </summary>
	/// <returns>The player data handler</returns>
    public PlayerDataHandler GetPlayerDataHandler()
    {
        return playerDataHandler;
    }

    /// <summary>
	/// Gets the plant database
	/// </summary>
	/// <returns></returns>
    public PlantDatabase GetPlantDatabase()
    {
        return plantDatabase;
    }

    void Update()
    {
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("SC_TEST_DebugMenu");
        }
    }
}
