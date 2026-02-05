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

    FMOD.Studio.EventInstance amb;
    FMOD.Studio.EventInstance musSpawn;
    FMOD.Studio.EventInstance musExploration;
    FMOD.Studio.EventInstance musBarbrook;
    FMOD.Studio.EventInstance musBarbrookPicnic;
    FMOD.Studio.EventInstance musNourPartA;
    FMOD.Studio.EventInstance musNourPartB;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        amb = RuntimeManager.CreateInstance(AmbEvent);
        amb.start();
    }

    public void StopAmb()
    {
        amb.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        amb.release();
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
        AudioManager.Instance.SetGlobalParameterByName("Zone", 1);
        musExploration = RuntimeManager.CreateInstance(MusExplorationEvent);
        musExploration.start();
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
        musBarbrookPicnic.start();
    }

    public void PlayMusNourPartA()
    {
        musNourPartA = RuntimeManager.CreateInstance(MusNourPartA);
        musNourPartA.start();
    }

    public void PlayMusNourPartB()
    {
        musNourPartB = RuntimeManager.CreateInstance(MusNourPartB);
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
                AudioManager.Instance.SetGlobalParameterByName("Zone", 2);
                PlayMusNourPartA();
                break;

            case CutSceneID.NourPartB:
                AudioManager.Instance.SetGlobalParameterByName("Zone", 2);
                PlayMusNourPartB();
                break;

            case CutSceneID.BarbrookPartA:
                //AudioManager.Instance.SetGlobalParameterByName("Zone", 2);
                PlayMusBarbrook();
                break;

            case CutSceneID.BarbrookPicNic:
                //AudioManager.Instance.SetGlobalParameterByName("Zone", 2);
                PlayMusBarbrookPicnic();
                break;

            case CutSceneID.Bed:
                AudioManager.Instance.SetGlobalParameterByName("Zone", 2);
                break;

            case CutSceneID.Drying:
                AudioManager.Instance.PlayEvent2D(EventID.DryPlant);
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
                //musBarbrook.release();
                break;

            case CutSceneID.BarbrookPicNic:
                AudioManager.Instance.PlayEvent2D(EventID.MusExploration2D);
                break;

            case CutSceneID.Bed:
                AudioManager.Instance.SetGlobalParameterByName("Zone", 1);
                break;

            case CutSceneID.Drying:
                break;

            case CutSceneID.Plant:
                break;
        }
    }

}
