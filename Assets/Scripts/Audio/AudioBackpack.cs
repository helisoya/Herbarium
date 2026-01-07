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
    [SerializeField] private EventID pinQuestOn;
    [SerializeField] private EventID pinQuestOff;
    [SerializeField] private EventID linkHover;
    [SerializeField] private EventID hintButtonHover;
    [SerializeField] private EventID hintHover;
    [SerializeField] private EventID hintClick;
    [SerializeField] private EventID hintReveal;
    [SerializeField] private EventID hintClose;
    [SerializeField] private EventID pageCresson;
    [SerializeField] private EventID pageMurailles;
    [SerializeField] private EventID pageAquaMint;


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

    public void PostPinQuest(bool pin)
    {
        if (pin == true)
        {
            AudioManager.Instance.Play2DEvent(pinQuestOn);
            Debug.Log("Son Pin Quest On");
        }

        else
        {
            AudioManager.Instance.Play2DEvent(pinQuestOff);
            Debug.Log("Son Pin Quest Off");
        }
            

    }

    public void PostLinkHover()
    {
        AudioManager.Instance.Play2DEvent(linkHover);
    }

    public void PostHintHover()
    {
        AudioManager.Instance.Play2DEvent(hintHover);
    }

    public void PostHintButtonHover()
    {
        AudioManager.Instance.Play2DEvent(hintButtonHover);
    }

    public void PostHintClick()
    {
        AudioManager.Instance.Play2DEvent(hintClick);
    }

    public void PostHintReveal()
    {
        AudioManager.Instance.Play2DEvent(hintReveal);
    }

    public void PostHintClose()
    {
        AudioManager.Instance.Play2DEvent(hintClose);
    }

    public void PostPlantInstrument(int plant)
    {
        if (plant == 0)
        {
            AudioManager.Instance.Play2DEvent(pageCresson);
        }
        
        if (plant == 1)
        {
            AudioManager.Instance.Play2DEvent(pageMurailles);
        }

        if (plant == 2)
        {
            AudioManager.Instance.Play2DEvent(pageAquaMint);
        }
    }

}
