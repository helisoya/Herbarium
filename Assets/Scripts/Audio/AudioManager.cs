using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioData audioData;


    private static AudioManager instance;
    public static AudioManager Instance => instance;

    private Dictionary<EventInstance, GameObject> tracked3DEvents = new();

    private bool ignoreseekspeed = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            sfxBus = RuntimeManager.GetBus(sfxBusString);
            masterBus = RuntimeManager.GetBus(masterBusString);
            musicBus = RuntimeManager.GetBus(musicBusString);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        // Met à jour automatiquement la position de tous les events 3D
        foreach (var kvp in tracked3DEvents)
        {
            EventInstance instance = kvp.Key;
            GameObject go = kvp.Value;

            if (!instance.isValid() || go == null)
            {
                // Libère les instances invalides ou dont le GameObject a été détruit
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                tracked3DEvents.Remove(instance);
                break; // safe dans foreach quand on modifie le dictionnaire
            }
            else
            {
                // Met à jour la position et la vélocité
                instance.set3DAttributes(RuntimeUtils.To3DAttributes(go));
            }
        }
    }

    public void PlayOneShot2D(EventID id)
    {
        RuntimeManager.PlayOneShot(audioData.events[id]);
    }

    public void PlayOneShot3D(EventID id, GameObject eventSource)
    {
        RuntimeManager.PlayOneShotAttached(audioData.events[id], eventSource);
    }

    public EventInstance PlayEvent2D(EventID id)
    {
        EventInstance instance = RuntimeManager.CreateInstance(audioData.events[id]);
        instance.start();
        return instance;
    }

    public EventInstance PlayEvent3D(EventID id, GameObject eventSource)
    {
        EventInstance instance = RuntimeManager.CreateInstance(audioData.events[id]);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(eventSource));
        instance.start();

        tracked3DEvents.Add(instance, eventSource);

        return instance;
    }

    public void Stop(EventInstance instance, FMOD.Studio.STOP_MODE mode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
    {
        if (!instance.isValid()) return;

        if (tracked3DEvents.ContainsKey(instance))
            tracked3DEvents.Remove(instance);

        instance.stop(mode);
        instance.release();
    }

    string masterBusString = "Bus:/";
    Bus masterBus;

    string sfxBusString = "Bus:/GlobalSFX";
    Bus sfxBus;

    string musicBusString = "Bus:/Music";
    Bus musicBus;

    public void SetMasterVolume(float volume)
    {
        
        masterBus.setVolume(volume);
        Debug.Log(volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        
        sfxBus.setVolume(volume);
        Debug.Log(volume);
    }

    public void SetMusicVolume(float volume)
    {
        
        musicBus.setVolume(volume);
        Debug.Log(volume);
    }

    public void MuteAll()
    {
        masterBus.setVolume(0);
    }

}