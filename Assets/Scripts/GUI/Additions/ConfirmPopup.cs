using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents a confirm popup
/// </summary>
public class ConfirmPopup : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject root;

    [Header("Audio")]
    [SerializeField] private UnityEvent onConfirm;
    [SerializeField] private UnityEvent onCancel;
    [SerializeField] private UnityEvent onHover;
    private Action endCallback;

    /// <summary>
    /// Opens the popup
    /// </summary>
    /// <param name="callback">The end callback</param>
    public void Open(Action callback)
    {
        root.SetActive(true);
        endCallback = callback;
    }

    /// <summary>
    /// Event for when hovering over something
    /// </summary>
    public void OnHover()
    {
        onHover.Invoke();
    }

    /// <summary>
    /// Confirms the action
    /// </summary>
    public void Confirm()
    {
        onConfirm.Invoke();
        endCallback.Invoke();
        Close();
    }

    /// <summary>
    /// Cancels the action
    /// </summary>
    public void Cancel()
    {
        onCancel.Invoke();
        Close();
    }

    /// <summary>
    /// Closes the popup
    /// </summary>
    public void Close()
    {
        root.SetActive(false);
    }
}
