using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the game's flow
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerDataHandler playerDataHandler;
    [SerializeField] private PlantDatabase plantDatabase;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Volume accessVolume;
    [SerializeField] private HerbariumCursor cursor;
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
            plantDatabase.Init();
            playerDataHandler.ResetData();

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
    /// Gets the access volume
    /// </summary>
    /// <returns>The volume</returns>
    public Volume GetAccessVolume()
    {
        return accessVolume;
    }

    /// <summary>
    /// Updates the access post process
    /// </summary>
    public void UpdateVolume()
    {
        LiftGammaGain gamma;
        accessVolume.profile.TryGet<LiftGammaGain>(out gamma);
        gamma.gamma.SetValue(new Vector4Parameter(new Vector4(1.0f, 1.0f, 1.0f, Settings.instance.GetCurrentGamma())));


        ColorLookup colorLookup;
        accessVolume.profile.TryGet<ColorLookup>(out colorLookup);
        colorLookup.contribution.SetValue(new FloatParameter(Settings.instance.IsNegativeColorFilterEnabled() ? 1.0f : 0.0f));
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
	/// <returns>The plant database</returns>
    public PlantDatabase GetPlantDatabase()
    {
        return plantDatabase;
    }

    /// <summary>
    /// Gets the cursor handler
    /// </summary>
    /// <returns>The cursor handler</returns>
    public HerbariumCursor GetCursor()
    {
        return cursor;
    }

    void Update()
    {
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("SC_TEST_DebugMenu");
        }
    }
}
