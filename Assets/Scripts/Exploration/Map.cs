using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a map
/// </summary>
public class Map : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private string ID;
    [SerializeField] private Spawnpoint[] spawnpoints;
    [SerializeField] private DialogGraph startupGraph;
    private bool isUpdatingCamera;
    private UnityEvent onRegrowthSystemRefresh;
    private UnityEvent<float,Color> onChangeHighlightInteractables;
    public bool started { get; private set; }

    public static Map instance { get; private set; }

    void Awake()
    {
        started = false;
        instance = this;

        onRegrowthSystemRefresh = new UnityEvent();
        onChangeHighlightInteractables = new UnityEvent<float,Color>();
    }

    void Start()
    {
        GameManager.instance.inMainMenu = false;
        if (GameManager.instance.loadingSave)
        {
            Player.instance.SetPosition(GameManager.instance.GetPlayerDataHandler().GetMapPosition());
            GameManager.instance.loadingSave = false;
        }
        else
        {
            FindPlayerSpawnPoint();
            GameManager.instance.GetPlayerDataHandler().SetCurrentMap(SceneManager.GetActiveScene().name);
        }

        isUpdatingCamera = true;
        CinemachineCore.UniformDeltaTimeOverride = 500;

        if (startupGraph != null) CutsceneManager.instance.ProcessCutscene(startupGraph,MusicManager.CutSceneID.Empty,gameObject);
        started = true;
    }

    /// <summary>
    /// Finds the current player's spawnpoint
    /// </summary>
    public void FindPlayerSpawnPoint()
    {
        Spawnpoint defaultSpawn = null;
        Spawnpoint selected = null;
        string currentMap = GameManager.instance.GetPlayerDataHandler().GetCurrentMap();

        for (int i = 0; i < spawnpoints.Length; i++)
        {
            if (spawnpoints[i].isDefaultSpawnpoint) defaultSpawn = spawnpoints[i];
            else if (spawnpoints[i].linkedMap.Equals(currentMap))
            {
                selected = spawnpoints[i];
                break;
            }
        }
        if (selected == null && defaultSpawn != null) selected = defaultSpawn;

        if (selected)
        {
            Player.instance.SetPosition(selected.transform.position);
        }
        else
        {
            Debug.LogError("No valid spawnpoint found. Did you forget to add a default ?");
        }
    }

    void LateUpdate()
    {
        if (isUpdatingCamera)
        {
            isUpdatingCamera = false;
            CinemachineCore.UniformDeltaTimeOverride = -1;
        }
    }

    /// <summary>
    /// Triggers the On Regrowth System Refresh event
    /// </summary>
    public void TriggerOnRegrowthSystemRefresh()
    {
        onRegrowthSystemRefresh.Invoke();
    }

    /// <summary>
    /// Registers a regrowth entity to the map
    /// </summary>
    /// <param name="entity">The entity</param>
    public void RegisterRegrowthEntity(RegrowthEntity entity)
    {
        onRegrowthSystemRefresh.AddListener(entity.RefreshEntity);
    }

    /// <summary>
    /// Unregister a regrowth entity to the map
    /// </summary>
    /// <param name="entity">The entity</param>
    public void UnRegisterRegrowthEntity(RegrowthEntity entity)
    {
        onRegrowthSystemRefresh.RemoveListener(entity.RefreshEntity);
    }

    /// <summary>
    /// Registers an interactable object
    /// </summary>
    /// <param name="obj">The object</param>
    public void RegisterInteractableObject(InteractableObject obj)
    {
        onChangeHighlightInteractables.AddListener(obj.SetHighlight);
    }

    /// <summary>
    /// Unregister an interactable object
    /// </summary>
    /// <param name="obj">The object</param>
    public void UnRegisterInteractableObject(InteractableObject obj)
    {
        onChangeHighlightInteractables.RemoveListener(obj.SetHighlight);
    }

    /// <summary>
    /// Triggers the on Change Highlight event
    /// </summary>
    /// <param name="strength">The highlight strength</param>
    /// <param name="color">The highlight color</param>
    public void TriggerOnChangeHighlight(float strength, Color color)
    {
        onChangeHighlightInteractables.Invoke(strength,color);
    }
}