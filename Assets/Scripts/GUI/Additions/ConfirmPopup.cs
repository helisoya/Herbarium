using System;
using UnityEngine;

/// <summary>
/// Represents a confirm popup
/// </summary>
public class ConfirmPopup : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject root;

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
    /// Applies the color
    /// </summary>
    public void Confirm()
    {
        endCallback.Invoke();
        Close();
    }

    /// <summary>
    /// Closes the picker
    /// </summary>
    public void Close()
    {
        root.SetActive(false);
    }
}
