using System.Collections;
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
    [SerializeField] private Fade fade;
    private bool exitingMainMenu = false;

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
        if(exitingMainMenu) return;

        GameManager.instance.loadingSave = true;
        GameManager.instance.GetPlayerDataHandler().LoadData();
        exitingMainMenu = true;
        StartCoroutine(RoutineTransitionToNextScene(GameManager.instance.GetPlayerDataHandler().GetCurrentMap()));
    }

    /// <summary>
    /// Starts a new game
    /// </summary>
    public void NewGame()
    {
        if(exitingMainMenu) return;

        GameManager.instance.loadingSave = false;
        GameManager.instance.GetPlayerDataHandler().ResetData();
        exitingMainMenu = true;
        StartCoroutine(RoutineTransitionToNextScene(newGameScene));
    }

    /// <summary>
    /// Routine for changing scenes
    /// </summary>
    /// <param name="nextScene">The next scene</param>
    private IEnumerator RoutineTransitionToNextScene(string nextScene)
    {
        fade.FadeTo(1);
        yield return new WaitForEndOfFrame();
        while (fade.fading)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(nextScene);
    }


    /// <summary>
    /// Opens the credits
    /// </summary>
    public void OpenCredits()
    {
        if(exitingMainMenu) return;

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
        if(exitingMainMenu) return;

        optionsMenu.Open();
    }
    
    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        if(exitingMainMenu) return;

        Application.Quit();
    }

}
