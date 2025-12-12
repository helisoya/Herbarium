using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the game's flow
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerDataHandler playerDataHandler;
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

            DontDestroyOnLoad(gameObject);
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


    void Update()
    {
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("SC_TEST_DebugMenu");
        }
    }
}
