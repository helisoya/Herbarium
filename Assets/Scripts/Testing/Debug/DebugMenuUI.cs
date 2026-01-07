using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the debug mode UI
/// It is used to quick travel between testing areas
/// </summary>
public class DebugMenuUI : MonoBehaviour
{

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
