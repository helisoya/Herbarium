using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class AudioUI : MonoBehaviour
{
    [SerializeField] EventID click;
    [SerializeField] EventID hover;
    [SerializeField] EventID back;
    [SerializeField] EventID toggleOn;
    [SerializeField] EventID toggleOff;
    [SerializeField] EventReference pause;

    public static AudioUI Instance;

    EventInstance pauseSnapshot;

    Bus Master;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        Master = RuntimeManager.GetBus("bus:/");
    }

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

    public void PostPauseSnapshot()
    {
        pauseSnapshot = RuntimeManager.CreateInstance(pause);
        pauseSnapshot.start();
    }

    public void StopPauseSnapshot()
    {
        pauseSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        pauseSnapshot.release();
    }

    public void OnMainMenu()
    {
        Master.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        MusicManager.Instance.StopMusExploration();
        MusicManager.Instance.StopAmb();
    }

   

}
