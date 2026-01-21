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
    [SerializeField] private string mainMenuScene;

    [Header("Dialog Logs")]
    [SerializeField] private Transform dialogLogsRoot;
    [SerializeField] private LocalizedText dialogLogPrefab;

    public bool isOpen{get{return root.activeInHierarchy;}}

    /// <summary>
    /// Opens the pause menu
    /// </summary>
    public void Open()
    {
        root.SetActive(true);
        ReloadDialogLogs(); 
    }

    /// <summary>
    /// Closes the pause menu
    /// </summary>
    public void Close()
    {
        root.SetActive(false);
        ClearDialogLogs();
    }

    private int CountVisibleCharacters(TMP_Text text)
    {
        int count = 0;
        foreach(TMP_LineInfo line in text.textInfo.lineInfo)
        {
            count += line.visibleCharacterCount + line.visibleSpaceCount;
        }
        print(count);
        return count;
    }

    /// <summary>
    /// Reloads the dialog logs on screen
    /// </summary>
    private void ReloadDialogLogs()
    {
        ClearDialogLogs();

        DialogLog[] logs = GameManager.instance.GetPlayerDataHandler().GetLog();
        LocalizedText instance;

        float totalSize = 0;
        float tmpSize;
        foreach(DialogLog log in logs)
        {
            instance = Instantiate(dialogLogPrefab,dialogLogsRoot);
            instance.SetInjectors(new object[]{Locals.GetLocal(log.speakerId), Locals.GetLocal(log.dialogId)},false);
            instance.SetNewKey("Pause_DialogLog");
            instance.GetText().ForceMeshUpdate(true,true);

            tmpSize = Mathf.Ceil(instance.GetText().textInfo.characterCount / 20f) * 30f;
            print(instance.GetText().textInfo.characterCount);
            instance.GetComponent<RectTransform>().sizeDelta = new Vector2(
                instance.GetComponent<RectTransform>().sizeDelta.x,
                tmpSize
            );
            totalSize += tmpSize;
        }

        dialogLogsRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            dialogLogsRoot.GetComponent<RectTransform>().sizeDelta.x,
            totalSize + dialogLogsRoot.GetComponent<VerticalLayoutGroup>().spacing * logs.Length
        );
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
        GameGUI.instance.ClosePause();
    }

    /// <summary>
    /// Click event for opening the options
    /// </summary>
    public void ClickOptions()
    {
        
    }

    /// <summary>
    /// Click event for going back to the main menu
    /// </summary>
    public void ClickMainMenu()
    {

        SceneManager.LoadScene(mainMenuScene);
    }

    /// <summary>
    /// Click even for quiting the menu
    /// </summary>
    public void ClickQuit()
    {
        Application.Quit();
    }
}
