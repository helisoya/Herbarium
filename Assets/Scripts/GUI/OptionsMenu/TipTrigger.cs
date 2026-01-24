using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Trigger for settings a tip in the options menu
/// </summary>
public class TipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string tipID;
    [SerializeField] private OptionsMenu optionsMenu;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        optionsMenu.ShowTip(tipID);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        optionsMenu.HideTip();
    }
}
