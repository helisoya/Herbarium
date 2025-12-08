using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioData audioData;
    
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
    }

    void Start()
    {
        //FMODUnity.RuntimeManager.LoadBank(audioData.banks[id], true);
    }
    
    public void Play2DEvent(EventID id)
    {
        RuntimeManager.PlayOneShot(audioData.events[id]);
    }

    public void Play3DEvent(EventID id, GameObject eventSource)
    {
        RuntimeManager.PlayOneShotAttached(audioData.events[id], eventSource);
    }



    public void LoadFMODBank(BankID id)
    {
        //RuntimeManager.StudioBankLoader(audioData.banks[id]);
    }
}
