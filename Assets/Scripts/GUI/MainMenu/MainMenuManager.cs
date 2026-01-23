using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Represents the main menu
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject generalRoot;
    [SerializeField] private GameObject creditsRoot;
    [SerializeField] private Button continueButton;
    [SerializeField] private string newGameScene;
    [SerializeField] private OptionsMenu optionsMenu;

    void Start()
    {
        GameManager.instance.inMainMenu = true;
        generalRoot.SetActive(true);
        creditsRoot.SetActive(false);
        continueButton.interactable = GameManager.instance.GetPlayerDataHandler().fileExistsOnDisk;
    }

    /// <summary>
    /// Resumes the game
    /// </summary>
    public void ResumeGame()
    {
        GameManager.instance.loadingSave = true;
        GameManager.instance.GetPlayerDataHandler().LoadData();
        SceneManager.LoadScene(GameManager.instance.GetPlayerDataHandler().GetCurrentMap());
    }

    /// <summary>
    /// Starts a new game
    /// </summary>
    public void NewGame()
    {
        GameManager.instance.loadingSave = false;
        GameManager.instance.GetPlayerDataHandler().ResetData();
        SceneManager.LoadScene(newGameScene);
    }

    /// <summary>
    /// Opens the credits
    /// </summary>
    public void OpenCredits()
    {
        generalRoot.SetActive(false);
        creditsRoot.SetActive(true);
    }

    /// <summary>
    /// Closes the credits
    /// </summary>
    public void CloseCredits()
    {
        generalRoot.SetActive(true);
        creditsRoot.SetActive(false);
    }

    /// <summary>
    /// Opens the options
    /// </summary>
    public void OpenOptions()
    {
        optionsMenu.Open();
    }
    
    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

}
