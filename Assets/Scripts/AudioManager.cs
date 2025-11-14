using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] private AudioData audioData;
    public void Play2DEvent(EventID id)
    {
        RuntimeManager.PlayOneShot(audioData.events[id]);
    }

    public void Play3DEvent(EventID id, GameObject eventSource = null)
    {
        RuntimeManager.PlayOneShotAttached(audioData.events[id], eventSource);
    }
}
