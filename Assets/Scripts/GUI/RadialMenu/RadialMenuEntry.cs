using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Represents an entry in the radial menu
/// </summary>
public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
    public DG.Tweening.Core.TweenerCore<Vector2, Vector2, DG.Tweening.Plugins.Options.VectorOptions> SetPosition(float x, float y, bool immediate)
    {
        
        if (immediate)
        {
            rectTransform.anchoredPosition = new Vector2(x, y);
            return null;
        } 
        else
        {
            return rectTransform.DOAnchorPos(new Vector2(x, y),0.30f).SetEase(Ease.OutQuad);
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
            rectTransform.DOScale(scale,0.3f).SetEase(Ease.OutQuad);
        }
        
    }

    /// <summary>
    /// Activate the entry (click)
    /// </summary>
    public void Activate()
    {
        if(canBeInteractedWith && onClick != null) onClick.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Activate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canBeInteractedWith)
        {
            rectTransform.DOComplete();
            rectTransform.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.OutQuad); 
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (canBeInteractedWith)
        {
            rectTransform.DOComplete();
            rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
        }
    }
}
