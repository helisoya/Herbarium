using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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
    [SerializeField] private ConfirmPopup confirmPopup;

    [Header("Audio")]
    [SerializeField] private UnityEvent onEnterMainMenu;
    [SerializeField] private UnityEvent onExitMainMenu;
    [SerializeField] private UnityEvent onHover;
    [SerializeField] private UnityEvent onClick;
    private bool exitingMainMenu = false;

    void Start()
    {
        onEnterMainMenu.Invoke();
        GameManager.instance.inMainMenu = true;
        generalRoot.SetActive(true);
        creditsRoot.SetActive(false);
        continueButton.interactable = GameManager.instance.GetPlayerDataHandler().fileExistsOnDisk;
    }


    /// <summary>
    /// Invokes On Hover Event
    /// </summary>
    public void OnHover()
    {
        onHover.Invoke();
    }

    /// <summary>
    /// Resumes the game
    /// </summary>
    public void ResumeGame()
    {
        if(exitingMainMenu) return;
        onClick.Invoke();
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
        onClick.Invoke();
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
        onExitMainMenu.Invoke();
        SceneManager.LoadScene(nextScene);
    }


    /// <summary>
    /// Opens the credits
    /// </summary>
    public void OpenCredits()
    {
        if(exitingMainMenu) return;
        onClick.Invoke();
        generalRoot.SetActive(false);
        creditsRoot.SetActive(true);
    }

    /// <summary>
    /// Closes the credits
    /// </summary>
    public void CloseCredits()
    {
        onClick.Invoke();
        generalRoot.SetActive(true);
        creditsRoot.SetActive(false);
    }

    /// <summary>
    /// Opens the options
    /// </summary>
    public void OpenOptions()
    {
        if(exitingMainMenu) return;
        onClick.Invoke();
        optionsMenu.Open();
    }
    
    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        if(exitingMainMenu) return;
        onClick.Invoke();
        confirmPopup.Open(CallbackQuit);
    }

    /// <summary>
    /// Callback for quiting the game
    /// </summary>
    public void CallbackQuit()
    {
        Application.Quit();
    }


    void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            if(optionsMenu.isOpen) optionsMenu.Close();
            else if(creditsRoot.activeInHierarchy) CloseCredits();
        }

    }

}
