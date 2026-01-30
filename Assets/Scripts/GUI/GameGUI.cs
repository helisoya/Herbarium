using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Represents the game's GUI
/// </summary>
public class GameGUI : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private PauseMenu pauseMenu;
    public bool isPauseOpen { get { return pauseMenu.isOpen; } }

    [Header("Quest pins")]
    [SerializeField] private QuestPin questPinPrefab;
    [SerializeField] private Transform questPinRoot;
    private List<QuestPin> questPins;

    [Header("Radial Menu")]
    [SerializeField] private RadialMenu radialMenu;
    public RadialMenuID currentRadialMenu {get{return radialMenu.currentRadialMenu;}}
    public int selectedRadialIndex {get{return radialMenu.selectedIndex;}}

    [Header("Herbarium")]
    [SerializeField] private HerbariumGUI herbariumGUI;
    public bool inHerbarium { get { return herbariumGUI.isOpen; } }

    [Header("HUD")]
    [SerializeField] private GameObject hudRoot;

    [Header("Popup")]
    [SerializeField] private CanvasGroup popupGroup;
    [SerializeField] private LocalizedText popupText;
    [SerializeField] private float popupFadeTime = 0.5f;
    [SerializeField] private float popupIdleTime = 2.0f;
    private Coroutine routinePopup;
    public bool showingPopup{get{return routinePopup != null;}}

    [Header("Dialog")]
    [SerializeField] private GameObject dialogRoot;
    [SerializeField] private Image dialogBg;
    [SerializeField] private GameObject dialogContinueRoot;
    [SerializeField] private LocalizedText dialogText;
    [SerializeField] private LocalizedText dialogNameText;
    [SerializeField] private LocalizedText dialogTitleText;
    [SerializeField] private GameObject dialogNameRoot;
    [SerializeField] private GameObject dialogTitleRoot;
    private Coroutine routineDialog;
    private bool skipDialog = false;
    public bool showingDialog { get { return routineDialog != null; } }


    [Header("Fading")]
    [SerializeField] private Fade fade;
    public bool fading { get { return fade.fading; } }


    [Header("Audio")]
    [SerializeField] private UnityEvent<string,string> onChangeDialogSpeaker;
    [SerializeField] private UnityEvent onStartTypingDialog;
    [SerializeField] private UnityEvent onStopTypingDialog;
    [SerializeField] private UnityEvent onOpenDialogWindow;
    [SerializeField] private UnityEvent onCloseDialogWindow;


    /*
    [Header("Choice")]
    [SerializeField] private GameObject choiceRoot;
    [SerializeField] private Transform choiceButtonsRoot;
    [SerializeField] private ChoiceButton choiceButtonPrefab;
    public int selectedChoiceIndex { get; private set; }*/

    public static GameGUI instance;


    void Awake()
    {
        instance = this;

        questPins = new List<QuestPin>();
    }

    void Start()
    {
        fade.ForceAlphaTo(1);
        fade.FadeTo(0);
        SetDialogBackgroundAlpha(Settings.instance.GetSubtitlesBackgroundOpacity());
        AddAllQuestsPin();
    }

    #region Links

    /*
    /// <summary>
    /// Opens the choice menu
    /// </summary>
    /// <param name="keys">The choice menu</param>
    public void OpenChoiceMenu(string[] keys)
    {
        selectedChoiceIndex = -1;

        foreach (Transform child in choiceButtonsRoot) Destroy(child.gameObject);
        for (int i = 0; i < keys.Length; i++)
        {
            ChoiceButton button = Instantiate(choiceButtonPrefab, choiceButtonsRoot);
            button.Init(i, keys[i]);
            if (i == 0) EventSystem.current.SetSelectedGameObject(button.gameObject);
        }

        choiceRoot.SetActive(true);
    }
   

    /// <summary>
    /// Selects a choice
    /// </summary>
    /// <param name="index">The choice's index</param>
    public void SelectChoice(int index)
    {
        onChoice.Invoke();
        selectedChoiceIndex = index;
        choiceRoot.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    */

    /// <summary>
    /// Disables the HUD
    /// </summary>
    public void DisableHud()
    {
        hudRoot.SetActive(false);
    }

    /// <summary>
    /// Enables the HUD if the settings allows it
    /// </summary>
    public void EnableHudIfPossible()
    {
        hudRoot.SetActive(Settings.instance.IsHUDEnabled());
    }

    /// <summary>
    /// Sets the dialog background's alpha
    /// </summary>
    /// <param name="alpha">The alpha</param>
    public void SetDialogBackgroundAlpha(float alpha)
    {
        Color newColor = dialogBg.color;
        newColor.a = alpha;
        dialogBg.color = newColor;
    }

    /// <summary>
    /// Fades the screen
    /// </summary>
    /// <param name="alpha">The alpha target</param>
    /// <param name="speed">The fading speed</param>
    public void FadeTo(float alpha, float speed = 2f)
    {
        fade.FadeTo(alpha, speed);
    }

    /// <summary>
    /// Sets the skip dialog tag to true
    /// </summary>
    public void SetSkipDialogTag()
    {
        skipDialog = true;
    }

    /// <summary>
    /// Opens the pause menu
    /// </summary>
    public void OpenPause()
    {
        DisableHud();
        pauseMenu.Open();
    }

    /// <summary>
    /// Closes the pause menu
    /// </summary>
    public void ClosePause()
    {
        EnableHudIfPossible();
        pauseMenu.Close();
    }

    /// <summary>
    /// Opens a radial menu
    /// </summary>
    /// <param name="data">The radial menu</param>
    public void OpenRadialMenu(RadialMenuData data)
    {
        DisableHud();
        radialMenu.Open(data);
    }

    /// <summary>
	/// Opens the default inventory radial menu
	/// </summary>
    public void OpenInventory()
    {
        DisableHud();
        radialMenu.OpenInventory();
    }

    /// <summary>
	/// Opens the give mode inventory radial menu
	/// </summary>
    public void OpenInventoryGive()
    {
        DisableHud();
        radialMenu.OpenInventoryGive();
    }

    /// <summary>
	/// Opens the default backpack radial menu
	/// </summary>
    public void OpenBackpack()
    {
        DisableHud();
        radialMenu.OpenBackpack();
    }

    /// <summary>
    /// Closes the radial menu
    /// </summary>
    public void CloseRadialMenu()
    {
        EnableHudIfPossible();
        radialMenu.Close();
    }

    /// <summary>
	/// Opens the herbarium
	/// </summary>
    public void OpenHerbarium()
    {
        DisableHud();
        herbariumGUI.Open();
    }

    /// <summary>
	/// Closes the Herbarium
	/// </summary>
    public void CloseHerbarium()
    {
        EnableHudIfPossible();
        herbariumGUI.Close();
    }

    /// <summary>
    /// Show a plant page in the herbarium
    /// </summary>
    /// <param name="pageIndex">The plant index</param>
    public void HerbariumShowPlantPage(int pageIndex)
    {
        herbariumGUI.SetPlant(pageIndex);
    }

    /// <summary>
    /// Show a quest page in the herbarium
    /// </summary>
    /// <param name="pageIndex">The quest index</param>
    public void HerbariumShowQuestPage(int questIndex)
    {
        herbariumGUI.SetQuest(questIndex);
    }

    /// <summary>
    /// Go left in the Herbarium
    /// </summary>
    public void HerbariumGoLeft()
    {
        if(inHerbarium) herbariumGUI.GoLeft();
    }

    /// <summary>
    /// Go right in the Herbarium
    /// </summary>
    public void HerbariumGoRight()
    {
        if(inHerbarium) herbariumGUI.GoRight();
    }


    /// <summary>
    /// Updates the radial menu
    /// </summary>
    /// <param name="mousePosition">The new mouse position</param>
    /// <param name="forceInteraction">True if the movement should also be counted as an interaction</param>
    public void UpdateRadial(Vector2 mousePosition, bool forceInteraction = false)
    {
        radialMenu.UpdateMousePosition(mousePosition,forceInteraction);
    }

    /// <summary>
    /// Activate the current radial menu's entry
    /// </summary>
    public void ActivateCurrentRadialMenuEntry()
    {
        radialMenu.ActivateCurrentlySelected();
    }

    #endregion

    #region Dialog

    /// <summary>
    /// Sets if the dialog panel is active or not
    /// </summary>
    /// <param name="value">True if it is active</param>
    public void SetDialogOpen(bool value, bool playSound = false)
    {
        dialogRoot.SetActive(value);

        if (playSound)
        {
            if(value) onOpenDialogWindow.Invoke();
            else onCloseDialogWindow.Invoke();
        }
    }

    /// <summary>
    /// Shows a dialog on screen
    /// </summary>
    /// <param name="dialogID">The dialog's ID</param>
    /// <param name="characterName">The character's name ID</param>
    /// <param name="characterTitle">The character's title ID</param>
    /// <param name="speakerAudio">The speaker's Audio ID</param>
    /// <param name="emotionAudio">The emotions's Audio ID</param>
    public void ShowDialog(string dialogID, string characterName, string characterTitle, string speakerAudio = null,string emotionAudio = null)
    {
        if (routineDialog != null) StopCoroutine(routineDialog);

        onChangeDialogSpeaker.Invoke(speakerAudio,emotionAudio);

        routineDialog = StartCoroutine(Routine_Dialog(dialogID,characterName,characterTitle));
    }



    /// <summary>
    /// Routine for showing a dialog
    /// </summary>
    /// <param name="dialogID">The dialog's ID</param>
    /// <param name="characterName">The character's name ID</param>
    /// <param name="characterTitle">The character's title ID</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator Routine_Dialog(string dialogID, string characterName, string characterTitle)
    {
        int charactersPerFrame = 1;
        float speed = 5f;
        skipDialog = false;


        SetDialogBackgroundAlpha(Settings.instance.GetSubtitlesBackgroundOpacity());
        SetDialogOpen(true);
        dialogContinueRoot.SetActive(false);

        if (string.IsNullOrEmpty(characterName))
        {
            dialogNameRoot.SetActive(false);
        }
        else
        {
            dialogNameRoot.SetActive(true);
            dialogNameText.SetNewKey(characterName);
        }

        if (string.IsNullOrEmpty(characterTitle))
        {
            dialogTitleRoot.SetActive(false);
        }
        else
        {
            dialogTitleRoot.SetActive(true);
            dialogTitleText.SetNewKey(characterTitle);
        }

        dialogText.SetNewKey(dialogID);
        TMP_Text txt = dialogText.GetText();

        int runsThisFrame = 0;

        txt.ForceMeshUpdate(false);
        TMP_TextInfo inf = txt.textInfo;
        int vis = 0;
        int max = inf.characterCount;
        int cpf = charactersPerFrame;

        onStartTypingDialog.Invoke();

        List<char> punctuation = new List<char>(new char[] { '.', ',', ';', '!', '?' });

        while (vis < max)
        {
            //allow skipping by increasing the characters per frame and the speed of occurance.
            if (skipDialog)
            {
                speed = 1;
                charactersPerFrame = charactersPerFrame < 5 ? 5 : charactersPerFrame + 3;
            }

            //reveal a certain number of characters per frame.
            while (runsThisFrame < charactersPerFrame)
            {
                vis++;
                txt.maxVisibleCharacters = vis;
                runsThisFrame++;
            }

            if (!skipDialog)
            {
                speed = punctuation.Contains(inf.characterInfo[vis - 1].character) ? 25 : 5;
            }

            //wait for the next available revelation time.
            runsThisFrame = 0;
            yield return new WaitForSeconds(0.01f * speed);
        }

        onStopTypingDialog.Invoke();

        dialogContinueRoot.SetActive(true);
        skipDialog = false;
        routineDialog = null;
    }

    #endregion

    #region Popup

    /// <summary>
    /// Shows a new popup
    /// </summary>
    /// <param name="key">The text key</param>
    /// <param name="injectors">The text injectors</param>
    public void ShowPopup(string key, object[] injectors)
    {
        if(routinePopup != null) StopCoroutine(routinePopup);
        routinePopup = StartCoroutine(Routine_Popup(key,injectors));
    }

    /// <summary>
    /// Shows a new popup (Routine)
    /// </summary>
    /// <param name="key">The text key</param>
    /// <param name="injectors">The text injectors</param>
    private IEnumerator Routine_Popup(string key, object[] injectors)
    {
        if(popupGroup.alpha > 0.0001f)
        {
            // Fade in the text before doing anything else
            yield return Routine_PopupFade(popupGroup.alpha,0.0f,popupFadeTime/popupGroup.alpha);
        }
        popupGroup.alpha = 0.0f;

        popupText.SetInjectors(injectors);
        popupText.SetNewKey(key);

        yield return Routine_PopupFade(popupGroup.alpha,1.0f,popupFadeTime);
        popupGroup.alpha = 1.0f;

        yield return new WaitForSeconds(popupIdleTime);

        yield return Routine_PopupFade(popupGroup.alpha,0.0f,popupFadeTime);
        popupGroup.alpha = 0.0f;

        routinePopup = null;
    }

    /// <summary>
    /// Internal routine for fading the popup
    /// </summary>
    /// <param name="start">The start opacity</param>
    /// <param name="end">The end opacity</param>
    /// <param name="duration">The opacity duration</param>
    private IEnumerator Routine_PopupFade(float start, float end, float duration)
    {
        for (float t = 0f; t <= duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            popupGroup.alpha = Mathf.Lerp(start,end,normalizedTime);

            yield return null;
        }
    }

    #endregion

    #region Quest Pins

    /// <summary>
    /// Adds all known pints
    /// </summary>
    public void AddAllQuestsPin()
    {
        Quest[] knownQuests = GameManager.instance.GetPlayerDataHandler().GetKnownQuests();
        QuestPin pin;

        foreach(Transform child in questPinRoot)
        {
            Destroy(child.gameObject);
        }

        questPins.Clear();

        foreach(Quest quest in knownQuests)
        {
            if (GameManager.instance.GetPlayerDataHandler().IsPinned(quest.id))
            {
                pin = Instantiate(questPinPrefab,questPinRoot);
                pin.Init(quest);
                questPins.Add(pin);
            }
        }
    }

    /// <summary>
    /// Refreshs all quest pins
    /// </summary>
    public void RefreshAllQuestsPins()
    {
        foreach(QuestPin pin in questPins)
        {
            pin.Refresh();
        }
    }

    /// <summary>
    /// Refreshs a specific pin
    /// </summary>
    /// <param name="questID">The pin's quest</param>
    public void RefreshQuestPin(string questID)
    {
        foreach(QuestPin pin in questPins)
        {
            if (pin.GetLinkedID().Equals(questID))
            {
                pin.Refresh();
                return;
            }
        }
    }

    /// <summary>
    /// Removes a specific quest pin
    /// </summary>
    /// <param name="questID">The pin's quest</param>
    public void RemovePin(string questID)
    {
        for(int i = 0; i < questPins.Count;i++)
        {
            if (questPins[i].GetLinkedID().Equals(questID))
            {
                Destroy(questPins[i].gameObject);
                questPins.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// Adds a quest pin
    /// </summary>
    /// <param name="questID">The pin's quest</param>
    public void AddPin(string questID)
    {
        for(int i = 0; i < questPins.Count;i++)
        {
            if (questPins[i].GetLinkedID().Equals(questID))
            {
                return;
            }
        }

        Quest quest = GameManager.instance.GetPlayerDataHandler().GetQuest(questID);
        QuestPin pin = Instantiate(questPinPrefab,questPinRoot);
        pin.Init(quest);
        questPins.Add(pin);
    }

    #endregion

    #region Click events

    /* ------------------------------------------------------- Click events ------------------------------------------------------- */

    /// <summary>
    /// Callback for setting the submit tag in a cutscene
    /// </summary>
    public void Event_CutsceneSubmit()
    {
        CutsceneManager.instance.UserSubmit();
    }

    /// <summary>
    /// Callback clicking on the backpack button
    /// </summary>
    public void Event_BackpackButton()
    {
        if(CutsceneManager.instance.inCutscene || !CutsceneManager.instance.inParrallelCutscene) return;
        Player.instance.StopPlayerMovements();
        OpenBackpack();
    }

    #endregion
}
