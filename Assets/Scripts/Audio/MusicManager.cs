using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public EventReference AmbEvent;
    public EventReference MusSpawnEvent;
    public EventReference MusExplorationEvent;
    public EventReference MusBarbrookEvent;
    public EventReference MusBarbrookPicnicEvent;
    public EventReference MusNourPartA;
    public EventReference MusNourPartB;
    public EventReference MusMainTitleEvent;

    EventInstance amb;
    EventInstance musSpawn;
    EventInstance musExploration;
    EventInstance musBarbrook;
    EventInstance musBarbrookPicnic;
    EventInstance musNourPartA;
    EventInstance musNourPartB;
    EventInstance musMainTitle;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        amb = RuntimeManager.CreateInstance(AmbEvent);
        amb.start();
        PlayMusExploration();
    }

    public void PlayAmb()
    {
        if (!amb.isValid())
        {
            amb = RuntimeManager.CreateInstance(AmbEvent);
            amb.start();
        }
        else return;
    }
    public void StopAmb()
    {
        if (amb.isValid())
        {
            amb.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            amb.release();
        }
        else return;
    }

    public void PlayMusMainTitle()
    {
        musMainTitle = RuntimeManager.CreateInstance(MusMainTitleEvent);
        musMainTitle.start();
    }

    public void StopMusMainTitle()
    {
        musMainTitle.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musMainTitle.release();
    }

    public void PlayMusSpawn()
    {
        musSpawn = RuntimeManager.CreateInstance(MusSpawnEvent);
        musSpawn.start();
    }

    public void StopMusSpawn()
    {
        musSpawn.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musSpawn.release();
    }

    public void PlayMusExploration()
    {
        if (!musExploration.isValid())
        {
            AudioManager.Instance.SetGlobalParameterByName("Zone", 0);
            musExploration = RuntimeManager.CreateInstance(MusExplorationEvent);
            Debug.Log("Je demande à la musique d'exploration de jouer");
            musExploration.start();
        }
        else
        {
            AudioManager.Instance.SetGlobalParameterByName("Zone", 1);
        }
        
    }

    public void FadeMusExploration(int zone)
    {
        AudioManager.Instance.SetGlobalParameterByName("Zone", zone);
    }

    public void StopMusExploration()
    {
        musExploration.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musExploration.release();
    }

    public void PlayMusBarbrook()
    {
        musBarbrook = RuntimeManager.CreateInstance(MusBarbrookEvent);
        musBarbrook.start();
    }

    public void PlayMusBarbrookPicnic()
    {
        musBarbrookPicnic = RuntimeManager.CreateInstance(MusBarbrookPicnicEvent);
        AudioManager.Instance.SetGlobalParameterByName("PicNic", 0);
        musBarbrookPicnic.start();
    }

    public void PlayMusNourPartA()
    {
        musNourPartA = RuntimeManager.CreateInstance(MusNourPartA);
        AudioManager.Instance.SetGlobalParameterByName("Map", 0);
        musNourPartA.start();
    }

    public void PlayMusNourPartB()
    {
        musNourPartB = RuntimeManager.CreateInstance(MusNourPartA);
        AudioManager.Instance.SetGlobalParameterByName("Map", 1);
        musNourPartB.start();
    }



    /// <summary>
    /// CUTSCENES
    /// </summary>

    public enum CutSceneID
    {
        Empty, NourPartA, NourPartB, BarbrookPartA, BarbrookPicNic, Bed, Drying, Plant,
    }

    public void PlayMusic()
    {

    }
    public void PostCutScene(CutSceneID cutScene)
    {
        switch (cutScene)
        {
            case CutSceneID.Empty:
                break;

            case CutSceneID.NourPartA:
                PlayMusNourPartA();
                break;

            case CutSceneID.NourPartB:
                PlayMusNourPartB();
                break;

            case CutSceneID.BarbrookPartA:
                PlayMusBarbrook();
                break;

            case CutSceneID.BarbrookPicNic:
                PlayMusBarbrookPicnic();
                break;

            case CutSceneID.Bed:
                AudioManager.Instance.SetGlobalParameterByName("Zone", 2);
                break;

            case CutSceneID.Drying:
                //AudioManager.Instance.PlayEvent2D(EventID.DryPlant);
                break;

            case CutSceneID.Plant:
                //StopMusExploration();
                //AudioManager.Instance.PlayEvent2D(EventID.MusForaging);
                break;
        }
    }

    public void EndCutScene(CutSceneID cutScene)
    {
        switch (cutScene)
        {
            case CutSceneID.Empty:
                break;

            case CutSceneID.NourPartA:
                AudioManager.Instance.PlayEvent2D(EventID.MusExploration2D);
                break;

            case CutSceneID.NourPartB:
                AudioManager.Instance.PlayEvent2D(EventID.MusExploration2D);
                break;

            case CutSceneID.BarbrookPartA:
                musBarbrook.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                musBarbrook.release();
                break;

            case CutSceneID.BarbrookPicNic:
                AudioManager.Instance.PlayEvent2D(EventID.MusExploration2D);
                break;

            case CutSceneID.Bed:
                AudioManager.Instance.SetGlobalParameterByName("Zone", 0);
                break;

            case CutSceneID.Drying:
                break;

            case CutSceneID.Plant:
                break;
        }
    }

}
