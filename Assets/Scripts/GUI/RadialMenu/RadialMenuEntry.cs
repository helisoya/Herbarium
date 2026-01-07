using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Represents an entry in the radial menu
/// </summary>
public class RadialMenuEntry : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private LocalizedText label;
    [SerializeField] private bool canBeInteractedWith;

    private RectTransform rectTransform;
    private Action onClick;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Initialize the entry
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="labelKey"></param>
    public void Init(RadialMenuEntryData data)
    {
        image.sprite = data.sprite;
        label.SetInjectors(data.injectors, false);
        label.SetNewKey(data.key);
        canBeInteractedWith = data.interactable;
        onClick = data.callback;
    }

    /// <summary>
    /// Sets the entry's callback
    /// </summary>
    /// <param name="callback">The new callback</param>
    public void SetCallback(Action callback)
    {
        onClick = callback;
    }

    /// <summary>
    /// Sets if the entry can be interacted with
    /// </summary>
    /// <param name="value">True if it can</param>
    public void SetCanBeInteractedWith(bool value)
    {
        canBeInteractedWith = value;
    }


    /// <summary>
    /// Sets the entry's position
    /// </summary>
    /// <param name="x">The X position</param>
    /// <param name="y">The Y position</param>
    /// <param name="immediate">True if the change must be immediate</param>
    public void SetPosition(float x, float y, bool immediate)
    {

        if (immediate)
        {
            rectTransform.anchoredPosition = new Vector2(x, y);
        }
        else
        {
            rectTransform.DOAnchorPos(new Vector2(x, y), 0.30f).SetEase(Ease.OutQuad);
        }
    }

    /// <summary>
    /// Sets the entry's scale
    /// </summary>
    /// <param name="scale">The scale</param>
    /// <param name="immediate">True if the change must be immediate</param>
    public void SetScale(Vector3 scale, bool immediate)
    {
        if (immediate)
        {
            rectTransform.localScale = scale;
        }
        else
        {
            rectTransform.DOScale(scale, 0.3f).SetEase(Ease.OutQuad);
        }

    }

    /// <summary>
    /// Activate the entry (click)
    /// </summary>
    /// <returns>True if the click was accepted</returns>
    public bool Activate()
    {
        if (canBeInteractedWith && onClick != null)
        {
            onClick.Invoke();
            return true;
        } 
        return false;
    }

    /// <summary>
    /// Highlight the entry
    /// </summary>
    public void Highlight()
    {
        if (canBeInteractedWith)
        {
            rectTransform.DOComplete();
            rectTransform.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.OutQuad);
        }

    }

    /// <summary>
    /// Stop highlighting the entry
    /// </summary>
    public void StopHighlight()
    {
        if (canBeInteractedWith)
        {
            rectTransform.DOComplete();
            rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
        }
    }
}
