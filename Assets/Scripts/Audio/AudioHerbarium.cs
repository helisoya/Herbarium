using UnityEngine;

public class AudioHerbarium : MonoBehaviour
{
    [SerializeField] private EventID herbariumOpen;
    [SerializeField] private EventID herbariumClose;
    [SerializeField] private EventID herbariumAmb;
    [SerializeField] private EventID pageTurnPrevious;
    [SerializeField] private EventID pageTurnNext;
    [SerializeField] private EventID pageHover;
    [SerializeField] private EventID plantsIndex;
    [SerializeField] private EventID plantsIndexHover;
    [SerializeField] private EventID questsIndex;
    [SerializeField] private EventID questsIndexHover;
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


    public void PostHerbariumOpen()
    {
        AudioManager.Instance.PlayOneShot2D(herbariumOpen);
    }

    public void PostHerbariumClose()
    {
        AudioManager.Instance.PlayOneShot2D(herbariumClose);
    }

    public void PostHerbariumAmb()
    {
        AudioManager.Instance.PlayOneShot2D(herbariumAmb);
    }

    public void PostPageTurnPrevious()
    {
        AudioManager.Instance.PlayOneShot2D(pageTurnPrevious);
    }

    public void PostPageTurnNext()
    {
        AudioManager.Instance.PlayOneShot2D(pageTurnNext);
    }
    public void PostPageHover()
    {
        AudioManager.Instance.PlayOneShot2D(pageHover);
    }

    public void PostPlantsIndex()
    {
        AudioManager.Instance.PlayOneShot2D(plantsIndex);
    }

    public void PostPlantsIndexHover()
    {
        AudioManager.Instance.PlayOneShot2D(plantsIndexHover);
    }

    public void PostQuestsIndex()
    {
        AudioManager.Instance.PlayOneShot2D(questsIndex);
    }

    public void PostQuestsIndexHover()
    {
        AudioManager.Instance.PlayOneShot2D(questsIndexHover);
    }

    public void PostPinQuest(bool pin)
    {
        if (pin == true)
        {
            AudioManager.Instance.PlayOneShot2D(pinQuestOn);
            Debug.Log("Son Pin Quest On");
        }

        else
        {
            AudioManager.Instance.PlayOneShot2D(pinQuestOff);
            Debug.Log("Son Pin Quest Off");
        }


    }

    public void PostLinkHover()
    {
        AudioManager.Instance.PlayOneShot2D(linkHover);
    }

    public void PostHintHover()
    {
        AudioManager.Instance.PlayOneShot2D(hintHover);
    }

    public void PostHintButtonHover()
    {
        AudioManager.Instance.PlayOneShot2D(hintButtonHover);
    }

    public void PostHintClick()
    {
        AudioManager.Instance.PlayOneShot2D(hintClick);
    }

    public void PostHintReveal()
    {
        AudioManager.Instance.PlayOneShot2D(hintReveal);
    }

    public void PostHintClose()
    {
        AudioManager.Instance.PlayOneShot2D(hintClose);
    }

    public void PostPlantInstrument(int plant)
    {
        if (plant == 0)
        {
            AudioManager.Instance.PlayOneShot2D(pageMurailles);
        }

        if (plant == 1)
        {
            AudioManager.Instance.PlayOneShot2D(pageCresson);
        }

        if (plant == 2)
        {
            AudioManager.Instance.PlayOneShot2D(pageAquaMint);
        }
    }
}
