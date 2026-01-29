using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private EventID AmbEvent;
    [SerializeField] private EventID ExploMusicEvent;

    public static MusicManager Instance;

    void Start()
    {
        AudioManager.Instance.PlayEvent2D(AmbEvent);
        AudioManager.Instance.PlayEvent2D(ExploMusicEvent);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public enum CutSceneID
    {
        Empty, NourPartA, NourPartB, BarbrookPartA, BarbrookPicNic, Bed, Drying, Plant,
    }
    public void PostCutScene(CutSceneID cutScene)
    {
        switch (cutScene)
        {
            case CutSceneID.Empty:
                break;

            case CutSceneID.NourPartA:
                AudioManager.Instance.SetGlobalParameterByName("Zone", 2);
                AudioManager.Instance.PlayEvent2D(EventID.MusNour);
                break;

            case CutSceneID.NourPartB:
                AudioManager.Instance.PlayEvent2D(EventID.MusNourPartB);
                break;

            case CutSceneID.BarbrookPartA:
                AudioManager.Instance.PlayEvent2D(EventID.MusBarbrook);
                break;

            case CutSceneID.BarbrookPicNic:
                break;

            case CutSceneID.Bed:
                AudioManager.Instance.SetGlobalParameterByName("Zone", 2);
                AudioManager.Instance.PlayEvent2D(EventID.MusGoodNight);
                break;

            case CutSceneID.Drying:
                break;

            case CutSceneID.Plant:
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
                AudioManager.Instance.PlayEvent2D(EventID.MusExploration2D);
                break;

            case CutSceneID.BarbrookPicNic:
                AudioManager.Instance.PlayEvent2D(EventID.MusExploration2D);
                break;

            case CutSceneID.Bed:
                AudioManager.Instance.PlayEvent2D(EventID.MusGoodMorning);
                break;

            case CutSceneID.Drying:
                AudioManager.Instance.PlayEvent2D(EventID.DryPlant);
                break;

            case CutSceneID.Plant:
                break;
        }
    }

}
