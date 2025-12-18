using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the player
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerInteraction interaction;
    public static Player instance;


    void Awake()
    {
        instance = this;
    }
    

    void OnMove(InputValue value)
    {
        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog) return;

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

        controller.SetMoveVector(vec);
    }

    void OnMousePosition(InputValue value)
    {
        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene)
        || GameGUI.instance.inHerbarium || GameGUI.instance.showingDialog) return;

        Vector2 mousePos = value.Get<Vector2>();

        if (GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED)
        {
            GameGUI.instance.UpdateRadial(mousePos);
            return;
        }

        interaction.SetMousePosition(mousePos);
        controller.SetMousePosition(mousePos);
    }

    void OnAttack(InputValue value)
    {
        if (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene)
        {
            CutsceneManager.instance.UserSubmit();
            return;
        }

        if (GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED)
        {
            if (value.isPressed) GameGUI.instance.ActivateCurrentRadialMenuEntry();
            return;
        }

        if (GameGUI.instance.inHerbarium || GameGUI.instance.showingDialog) return;

        bool shouldHold = Settings.instance.IsHoldModeEnabled();

        if (shouldHold) controller.SetTryToMoveUsingCursor(value.isPressed);
        else if (value.isPressed) controller.ToggleTryToMoveUsingCursor();

        if (value.isPressed) interaction.TryInterract();
    }

    void OnBackpack(InputValue value)
    {
        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog) return;

        if (GameGUI.instance.currentRadialMenu != RadialMenuID.BACKPACK)
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
        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog) return;

        if (GameGUI.instance.currentRadialMenu != RadialMenuID.INVENTORY)
        {
            if (value.isPressed)
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
        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog) return;

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


    void OnPause(InputValue value)
    {
        if ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || GameGUI.instance.showingDialog) return;

        if(GameGUI.instance.currentRadialMenu != RadialMenuID.CLOSED)
        {
            GameGUI.instance.CloseRadialMenu();
            return;
        }

        if (GameGUI.instance.inHerbarium)
        {
            GameGUI.instance.CloseHerbarium();
            return;
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

    public void StopPlayerMovements()
    {
        controller.SetMoveVector(Vector2.zero);
        controller.SetTryToMoveUsingCursor(false);
    }
}
