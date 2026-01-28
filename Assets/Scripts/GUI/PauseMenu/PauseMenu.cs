using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Represents the pause menu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject root;
    [SerializeField] private OptionsMenu optionsMenu;
    [SerializeField] private string mainMenuScene;
    [SerializeField] private Fade linkedFade;
    [SerializeField] private ConfirmPopup popup;

    [Header("Dialog Logs")]
    [SerializeField] private Transform dialogLogsRoot;
    [SerializeField] private LocalizedText dialogLogPrefab;

    public bool isOpen{get{return root.activeInHierarchy;}}
    private bool exitingToMainMenu;

    /// <summary>
    /// Opens the pause menu
    /// </summary>
    public void Open()
    {
        Time.timeScale = 0;
        root.SetActive(true);
        ReloadDialogLogs(); 
    }

    /// <summary>
    /// Closes the pause menu
    /// </summary>
    public void Close()
    {
        Time.timeScale = 1;
        root.SetActive(false);
        optionsMenu.Close();
        ClearDialogLogs();
    }

    /// <summary>
    /// Reloads the dialog logs on screen
    /// </summary>
    private void ReloadDialogLogs()
    {
        ClearDialogLogs();

        DialogLog[] logs = GameManager.instance.GetPlayerDataHandler().GetLog();
        LocalizedText instance;

        foreach(DialogLog log in logs)
        {
            instance = Instantiate(dialogLogPrefab,dialogLogsRoot);
            instance.SetInjectors(new object[]{Locals.GetLocal(log.speakerId), Locals.GetLocal(log.dialogId)},false);
            instance.SetNewKey("Pause_DialogLog");
            instance.GetText().ForceMeshUpdate(true,true);
        }
    }

    /// <summary>
    /// Clears the dialog logs on screen
    /// </summary>
    private void ClearDialogLogs()
    {
        foreach(Transform child in dialogLogsRoot)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Click event for resuming the game
    /// </summary>
    public void ClickResume()
    {
        if(exitingToMainMenu) return;
        Close();
    }

    /// <summary>
    /// Click event for opening the options
    /// </summary>
    public void ClickOptions()
    {
        if(exitingToMainMenu) return;
        optionsMenu.Open();
    }

    /// <summary>
    /// Click event for going back to the main menu
    /// </summary>
    public void ClickMainMenu()
    {
        if(exitingToMainMenu) return;
        popup.Open(CallbackMainMenu);
    }

    /// <summary>
    /// Routine for exiting to the main menu
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator RoutineMainMenu()
    {
        Time.timeScale = 1;

        linkedFade.FadeTo(1);
        yield return new WaitForEndOfFrame();
        while (linkedFade.fading)
        {
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(mainMenuScene);
    }

    /// <summary>
    /// Click even for quiting the menu
    /// </summary>
    public void ClickQuit()
    {
        if(exitingToMainMenu) return;
        popup.Open(CallbackQuit);
    }

    /// <summary>
    /// Callback on confirming the exit to main menu
    /// </summary>
    public void CallbackMainMenu()
    {
        exitingToMainMenu = true;
        StartCoroutine(RoutineMainMenu());
    }


    /// <summary>
    /// Callback on confirming the exit to desktop
    /// </summary>
    public void CallbackQuit()
    {
        Application.Quit();
    }
}
