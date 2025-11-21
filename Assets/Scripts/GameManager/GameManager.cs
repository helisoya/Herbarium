using UnityEngine;

/// <summary>
/// Handles the game's flow
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            
            Locals.Init();
            Settings.Init();

            DontDestroyOnLoad(gameObject);
        }
    }
}
