using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the game's GUI
/// </summary>
public class GameGUI : MonoBehaviour
{
    [Header("Pause")]
    //[SerializeField] private PauseMenu pauseMenu;
    //public bool isPauseOpen { get { return pauseMenu.isOpen; } }

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
    [SerializeField] private LocalizedText dialogText;
    private Coroutine routineDialog;
    private bool skipDialog = false;
    public bool showingDialog { get { return routineDialog != null; } }


    [Header("Fading")]
    [SerializeField] private Fade fade;
    public bool fading { get { return fade.fading; } }


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
    }

    void Start()
    {
        fade.ForceAlphaTo(1);
        fade.FadeTo(0);
        SetDialogBackgroundAlpha(Settings.instance.GetSubtitlesBackgroundOpacity());
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
        Time.timeScale = 0f;
        //pauseMenu.Open();
    }

    /// <summary>
    /// Closes the pause menu
    /// </summary>
    public void ClosePause()
    {
        EnableHudIfPossible();
        Time.timeScale = 1f;
        //pauseMenu.Close();
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
    public void SetDialogOpen(bool value)
    {
        if(value) DisableHud();
        else EnableHudIfPossible();
        dialogRoot.SetActive(value);
    }

    /// <summary>
    /// Shows a dialog on screen
    /// </summary>
    /// <param name="dialogID">The dialog's ID</param>
    public void ShowDialog(string dialogID)
    {
        if (routineDialog != null) StopCoroutine(routineDialog);
        routineDialog = StartCoroutine(Routine_Dialog(dialogID));
    }



    /// <summary>
    /// Routine for showing a dialog
    /// </summary>
    /// <param name="dialogID">The dialog's ID</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator Routine_Dialog(string dialogID)
    {
        int charactersPerFrame = 1;
        float speed = 5f;
        skipDialog = false;


        SetDialogBackgroundAlpha(Settings.instance.GetSubtitlesBackgroundOpacity());
        SetDialogOpen(true);
        dialogText.SetNewKey(dialogID);
        TMP_Text txt = dialogText.GetText();

        int runsThisFrame = 0;

        txt.ForceMeshUpdate(false);
        TMP_TextInfo inf = txt.textInfo;
        int vis = 0;
        int max = inf.characterCount;
        int cpf = charactersPerFrame;

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
