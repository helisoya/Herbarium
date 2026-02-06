using System;
using FMODUnity;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

/// <summary>
/// Represents the player
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerInteraction interaction;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private CinemachineCamera cinemachineCamera;


    private MicroInteraction currentMicroInteraction;
    private string currentMicroInteractionScene;
    private bool pointerOverGUI;
    public static Player instance;

    public Vector3 position{get{return controller.GetBody().position;}}
    public bool inMicroInteraction{get; private set;}
    public MicroInteraction.EndingType lastMicroInteractionEnding {get; private set;}

    private Renderer[] renderers;

    public bool canComponentsUpdate
    {
        get
        {
            return !GameGUI.instance.isPauseOpen && !inMicroInteraction && (!CutsceneManager.instance.inCutscene || CutsceneManager.instance.inParrallelCutscene) &&
            GameGUI.instance.currentRadialMenu == RadialMenuID.CLOSED && !GameGUI.instance.inHerbarium && !GameGUI.instance.isPauseOpen;
        }
    }

    void Awake()
    {
        instance = this;
        currentMicroInteraction = null;
        currentMicroInteractionScene = null;
        lastMicroInteractionEnding = MicroInteraction.EndingType.CANCEL;
        renderers = GetComponentsInChildren<Renderer>();
        renderers = Array.FindAll(renderers, r => !(r is VFXRenderer));
    }

    void Start()
    {
        SetHighlight(Settings.instance.GetPlayerOutlineActive() ? Settings.instance.GetPlayerOutlineStrength() : 0.0f, Settings.instance.GetPlayerOutlineColor());
    }

    /// <summary>
    /// Sets the highlight for an interactable
    /// </summary>
    /// <param name="strength">The highlight's strength</param>
    /// <param name="color">The highlight's color</param>
    public void SetHighlight(float strength, Color color)
    {
        foreach(Renderer renderer in renderers)
        {
            renderer.material.SetFloat("_HighlightStrength",strength);
            renderer.material.SetColor("_HighlightColor",color);
        }
    }

    /// <summary>
    /// Sets the camera's target
    /// </summary>
    /// <param name="target">The new target</param>
    public void SetCameraTarget(Transform target)
    {
        cinemachineCamera.Target.TrackingTarget = target;
        cinemachineCamera.Target.LookAtTarget = target;
    }

    /// <summary>
    /// Resets the camera's target to the player body
    /// </summary>
    public void ResetCameraTarget()
    {
        cinemachineCamera.Target.TrackingTarget = controller.GetBody().transform;
        cinemachineCamera.Target.LookAtTarget = controller.GetBody().transform;
    }
    

    void OnMove(InputValue value)
    {
        if (inMicroInteraction)
        {
            if(currentMicroInteraction) currentMicroInteraction.ForwardInput(MicroInteraction.InputType.MoveKeys,value);
            return;
        }

        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog || GameGUI.instance.mapOpen) return;

        Vector2 vec = value.Get<Vector2>();

        if(GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED)
        {
            if(Mathf.Abs(vec.x) >= 0.95f ||Mathf.Abs(vec.y) >= 0.95f)
            {
                GameGUI.instance.UpdateRadial(new Vector2(
                    Screen.width / 2f + vec.x * 100f,
                    Screen.height / 2f + vec.y * 100f
                ),true);
            }
            return;
        }

        if (GameGUI.instance.inHerbarium)
        {
            if(vec.x >= 0.95f)
            {
                GameGUI.instance.HerbariumGoRight();
            }
            else if(vec.x <= -0.95f)
            {
                GameGUI.instance.HerbariumGoLeft();
            }
            return;
        }

        if (GameGUI.instance.isPauseOpen)
        {
            float delta = vec.x >= 0.95f ? 1 : (vec.x <= -0.95f ? -1 : 0);
            if(delta != 0.0f) GameGUI.instance.OptionsMove(delta);
        }

        controller.SetMoveVector(vec);
    }


    void OnResetAll(InputValue value)
    {
        if (inMicroInteraction)
        {
            if(currentMicroInteraction) currentMicroInteraction.ForwardInput(MicroInteraction.InputType.ResetAll,value);
            return;
        }

        if (GameGUI.instance.isPauseOpen)
        {
            GameGUI.instance.OptionsResetAll();
        }
    }

    void OnMousePosition(InputValue value)
    {
        if(GameGUI.instance.isPauseOpen || GameGUI.instance.mapOpen) return;

        if(inMicroInteraction){
            if(currentMicroInteraction) currentMicroInteraction.ForwardInput(MicroInteraction.InputType.MousePosition,value);
            return;
        } 

        if(GameGUI.instance.inHerbarium || GameGUI.instance.showingDialog) return;

        Vector2 mousePos = value.Get<Vector2>();

        if (GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED)
        {
            GameGUI.instance.UpdateRadial(mousePos);
            return;
        }

        if (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) return;

        interaction.SetMousePosition(mousePos);
        controller.SetMousePosition(mousePos);
    }

    void OnAttack(InputValue value)
    {
        if(GameGUI.instance.isPauseOpen || GameGUI.instance.mapOpen) return;
        
        if(inMicroInteraction)
        { 
            if(currentMicroInteraction) currentMicroInteraction.ForwardInput(MicroInteraction.InputType.MouseLeftClick,value);
            return;
        }

        if (GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED)
        {
            if (value.isPressed) GameGUI.instance.ActivateCurrentRadialMenuEntry();
            return;
        }

        if (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene)
        {
            if(value.isPressed) CutsceneManager.instance.UserSubmit();
            return;
        }

        if (GameGUI.instance.inHerbarium || GameGUI.instance.showingDialog || pointerOverGUI) return;

        if(value.isPressed && interaction.TryInterract()) return;

        bool toggleEnabled = Settings.instance.IsToggleMoveEnabled();

        if (!toggleEnabled) controller.SetUpdateTargetWithMouse(value.isPressed);
        else if (value.isPressed) controller.ToggleUpdateTargetWithMouse();
        
        if (value.isPressed)
            controller.PlayMouseTargetVFX();
    }

    void OnBackpack(InputValue value)
    {
        if(GameGUI.instance.isPauseOpen || GameGUI.instance.mapOpen) return;

        if (inMicroInteraction)
        {
            if(currentMicroInteraction) currentMicroInteraction.ForwardInput(MicroInteraction.InputType.MouseRightClick,value);
            return;
        }

        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog) return;

        if(GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED && value.isPressed)
        {
            GameGUI.instance.CloseRadialMenu();
            return;
        }

        if (GameGUI.instance.currentRadialMenu != RadialMenuID.BACKPACK && GameGUI.instance.currentRadialMenu != RadialMenuID.GIVE)
        {
            if (value.isPressed)
            {
                StopPlayerMovements();
                if(GameGUI.instance.inHerbarium) GameGUI.instance.CloseHerbarium();
                GameGUI.instance.OpenBackpack();
            }
            return;
        }
    }

    void OnInventory(InputValue value)
    {
        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog || inMicroInteraction || GameGUI.instance.isPauseOpen || GameGUI.instance.mapOpen) return;

        if (GameGUI.instance.currentRadialMenu != RadialMenuID.INVENTORY && GameGUI.instance.currentRadialMenu != RadialMenuID.GIVE)
        {
            if (value.isPressed && GameGUI.instance.currentRadialMenu == RadialMenuID.CLOSED)
            {
                StopPlayerMovements();
                if(GameGUI.instance.inHerbarium) GameGUI.instance.CloseHerbarium();
                GameGUI.instance.OpenInventory();
            }
            return;
        }
    }

    void OnHerbarium(InputValue value)
    {
        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog || inMicroInteraction || GameGUI.instance.isPauseOpen || GameGUI.instance.mapOpen) return;

        if (!GameGUI.instance.inHerbarium)
        {
            if (value.isPressed)
            {
                StopPlayerMovements();
                if(GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED) GameGUI.instance.CloseRadialMenu();
                GameGUI.instance.OpenHerbarium();
            }
            return;
        }
    }

    void OnMap(InputValue value)
    {
        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog || inMicroInteraction || GameGUI.instance.isPauseOpen) return;

        if (!GameGUI.instance.mapOpen && GameManager.instance.GetPlayerDataHandler().IsMapUnlocked())
        {
            if (value.isPressed)
            {
                StopPlayerMovements();
                if(GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED) GameGUI.instance.CloseRadialMenu();
                if(GameGUI.instance.inHerbarium) GameGUI.instance.CloseHerbarium();
                GameGUI.instance.OpenMap();
            }
            return;
        }
    }


    void OnPause(InputValue value)
    {
        if(inMicroInteraction)
        { 
            if(currentMicroInteraction) currentMicroInteraction.ForwardInput(MicroInteraction.InputType.Pause,value);
            return;
        }

        if(GameGUI.instance.showingDialog) return;

        if (GameGUI.instance.mapOpen)
        {
            GameGUI.instance.CloseHerbarium();
            return;
        }

        if (GameGUI.instance.inHerbarium)
        {
            GameGUI.instance.CloseHerbarium();
            return;
        }

        if(GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED && GameGUI.instance.currentRadialMenu != RadialMenuID.GIVE)
        {
            GameGUI.instance.CloseRadialMenu();
            return;
        }

        if (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) return;

        if (GameGUI.instance.isPauseOpen)
        {
            GameGUI.instance.ClosePause();
        }
        else
        {
            GameGUI.instance.OpenPause();
        }
    }

    void OnSwitchHerbarium(InputValue value)
    {
        if(value.isPressed && GameGUI.instance.inHerbarium)
        {
            GameGUI.instance.HerbariumSwitchTabs();
        }
    }

    /// <summary>
    /// Starts a micro interaction
    /// </summary>
    /// <param name="sceneName">The interaction's scene name</param>
    /// <param name="plantId">The plant id for the interaction</param>
    public void StartMicroInteraction(string sceneName, string plantId)
    {
        GameGUI.instance.DisableHud();
        inMicroInteraction = true;
        currentMicroInteractionScene = sceneName;
        SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive).completed += (x) =>
        {
            Scene s = SceneManager.GetSceneByName(sceneName);
            GameObject[] gameObjects = s.GetRootGameObjects();
            foreach(GameObject obj in gameObjects)
            {
                if(obj.TryGetComponent<MicroInteraction>(out MicroInteraction interaction))
                {
                    StartMicroInteraction(interaction,plantId);
                    break;
                }
            }
        };
    }

    /// <summary>
    /// Starts a micro interaction
    /// </summary>
    /// <param name="microInteraction">The interaction</param>
    /// <param name="plantId">The plant id for the interaction</param>
    public void StartMicroInteraction(MicroInteraction microInteraction, string plantId)
    {
        GameGUI.instance.DisableHud();
        inMicroInteraction = true;
        playerCamera.enabled = false;
        if (playerCamera.TryGetComponent<StudioListener>(out StudioListener listener))
        {
            Destroy(listener);
        }
        currentMicroInteraction = microInteraction;
        microInteraction.StartInteraction(plantId);
    }

    /// <summary>
    /// Ends a micro interaction
    /// </summary>
    /// <param name="endingType">The micro interaction's ending type</param>
    public void StopMicroInteraction(MicroInteraction.EndingType endingType)
    {
        if(currentMicroInteractionScene != null)
        {
            SceneManager.UnloadSceneAsync(currentMicroInteractionScene).completed += (x) =>
            {
                // Once the scene is unloaded, restart the camera and hook up the interaction results to the other modules of the game
                lastMicroInteractionEnding = endingType;
                GameGUI.instance.EnableHudIfPossible();
                currentMicroInteraction = null;
                currentMicroInteractionScene = null;
                playerCamera.enabled = true;
                if (!playerCamera.GetComponent<StudioListener>())
                {
                    playerCamera.gameObject.AddComponent<StudioListener>().AttenuationObject = controller.GetBody().gameObject;
                }
                inMicroInteraction = false;
            };
        }
        else
        {
            // Technically, this is only true in debug conditions
            // (When you only play the foraging part an have no concern with the exploration part)
            lastMicroInteractionEnding = endingType;
            GameGUI.instance.EnableHudIfPossible();
            currentMicroInteraction = null;
            currentMicroInteractionScene = null;
            inMicroInteraction = false;
        }
    }


    /// <summary>
    /// Sets the player's move vector
    /// </summary>
    /// <param name="vector">The new move vector</param>
    public void SetMovementVector(Vector2 vector)
    {
        controller.SetMoveVector(vector);
    }

    /// <summary>
    /// Sets the controller's position
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position)
    {
        cinemachineCamera.enabled = false;
        controller.SetPosition(position);
    }

    void LateUpdate()
    {
        if (!cinemachineCamera.enabled)
        {
            cinemachineCamera.PreviousStateIsValid = false;
            cinemachineCamera.enabled = true;
            CinemachineCore.UniformDeltaTimeOverride = -1;
        }

        pointerOverGUI = EventSystem.current.IsPointerOverGameObject();
    }

    /// <summary>
    /// Stops all player movements
    /// </summary>
    public void StopPlayerMovements()
    {
        controller.SetMoveVector(Vector2.zero);
        controller.SetUpdateTargetWithMouse(false);
    }
}
