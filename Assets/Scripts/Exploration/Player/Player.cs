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
        if((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) ||
            GameGUI.instance.radialMenuOpen) return;

        controller.SetMoveVector(value.Get<Vector2>());
    }

    void OnMousePosition(InputValue value)
    {
        if((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene)) return;

        Vector2 mousePos = value.Get<Vector2>();

        if (GameGUI.instance.radialMenuOpen)
        {
            GameGUI.instance.UpdateRadial(mousePos);
            return;
        }

        interaction.SetMousePosition(mousePos);
        controller.SetMousePosition(mousePos);
    }

    void OnAttack(InputValue value)
    {
        if(CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene){
            CutsceneManager.instance.UserSubmit();
            return;
        }

        if (GameGUI.instance.radialMenuOpen)
        {
            if(value.isPressed) GameGUI.instance.ActivateCurrentRadialMenuEntry();
            return;
        } 

        bool shouldHold = Settings.instance.IsHoldModeEnabled();

        if(shouldHold) controller.SetTryToMoveUsingCursor(value.isPressed);
        else if(value.isPressed) controller.ToggleTryToMoveUsingCursor();

        if(value.isPressed) interaction.TryInterract();
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
