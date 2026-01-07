using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the game's flow
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerDataHandler playerDataHandler;
    [SerializeField] private PlantDatabase plantDatabase;
    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            Locals.Init();
            Settings.Init();
            playerDataHandler.ResetData();
            plantDatabase.Init();

            SetPersistent(gameObject);



            // Debug build
            if (Debug.isDebugBuild)
            {
                playerDataHandler.AddHerbariumPage("TestPlant1");
                playerDataHandler.AddHerbariumPage("TestPlant2");
                playerDataHandler.SetVariable("quest_test",0);
            }
        }
    }

    /// <summary>
    /// Sets the object as persistent
    /// </summary>
    /// <param name="obj">The object</param>
    private void SetPersistent(GameObject obj)
    {
        DontDestroyOnLoad(obj);
        foreach(Transform child in obj.transform)
        {
            SetPersistent(child.gameObject);
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
