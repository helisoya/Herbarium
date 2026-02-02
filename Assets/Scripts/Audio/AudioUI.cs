using UnityEngine;

public class AudioUI : MonoBehaviour
{
    [SerializeField] EventID click;
    [SerializeField] EventID hover;
    [SerializeField] EventID back;
    [SerializeField] EventID toggleOn;
    [SerializeField] EventID toggleOff;

    public void PostClick()
    {
        AudioManager.Instance.PlayOneShot2D(click);
    }

    public void PostHover()
    {
        AudioManager.Instance.PlayOneShot2D(hover);
    }

    public void PostBack()
    {
        AudioManager.Instance.PlayOneShot2D(back);
    }

    public void PostToggle(bool state)
    {
        if (state) AudioManager.Instance.PlayOneShot2D(toggleOn);
        else AudioManager.Instance.PlayOneShot2D(toggleOff);
    }

    
}
