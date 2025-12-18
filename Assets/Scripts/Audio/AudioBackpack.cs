using UnityEngine;

public class AudioBackpack : MonoBehaviour
{
    [SerializeField] private EventID backpackOpen;
    [SerializeField] private EventID backpackClose;
    [SerializeField] private EventID backpackHover;
    [SerializeField] private EventID backpackClick;
    [SerializeField] private EventID backpackBack;
    [SerializeField] private EventID inventoryOpen;
    [SerializeField] private EventID inventoryClose;
    [SerializeField] private EventID inventoryHover;
    [SerializeField] private EventID herbariumOpen;
    [SerializeField] private EventID herbariumClose;
    [SerializeField] private EventID herbariumAmb;
    [SerializeField] private EventID pageTurnPrevious;
    [SerializeField] private EventID pageTurnNext;
    [SerializeField] private EventID pageHover;
    [SerializeField] private EventID plantsIndex;
    [SerializeField] private EventID questsIndex;

    public void PostBackpackOpen()
    {
        AudioManager.Instance.Play2DEvent(backpackOpen);
    }

    public void PostBackpackClose()
    {
        AudioManager.Instance.Play2DEvent(backpackClose);
    }

    public void PostBackpackHover()
    {
        AudioManager.Instance.Play2DEvent(backpackHover);
    }

    public void PostBackpackClick()
    {
        AudioManager.Instance.Play2DEvent(backpackClick);
    }

    public void PostBackpackBack()
    {
        AudioManager.Instance.Play2DEvent(backpackBack);
    }

    public void PostInventoryOpen()
    {
        AudioManager.Instance.Play2DEvent(inventoryOpen);
    }

    public void PostInventoryClose()
    {
        AudioManager.Instance.Play2DEvent(inventoryClose);
    }

    public void PostHerbariumOpen()
    {
        AudioManager.Instance.Play2DEvent(herbariumOpen);
    }

    public void PostHerbariumClose()
    {
        AudioManager.Instance.Play2DEvent(herbariumClose);
    }

    public void PostHerbariumAmb()
    {
        AudioManager.Instance.Play2DEvent(herbariumAmb);
    }

    public void PostPageTurnPrevious()
    {
        AudioManager.Instance.Play2DEvent(pageTurnPrevious);
    }

    public void PostPageTurnNext()
    {
        AudioManager.Instance.Play2DEvent(pageTurnNext);
    }
    public void PostPageHover()
    {
        AudioManager.Instance.Play2DEvent(pageHover);
    }

    public void PostPlantsIndex()
    {
        AudioManager.Instance.Play2DEvent(plantsIndex);
    }

    public void PostQuestsIndex()
    {
        AudioManager.Instance.Play2DEvent(questsIndex);
    }

}
