using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private EventID AmbEvent;
    [SerializeField] private EventID ExploMusicEvent;

    void Start()
    {
        AudioManager.Instance.PlayEvent2D(AmbEvent);
        AudioManager.Instance.PlayEvent2D(ExploMusicEvent);
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
                AudioManager.Instance.PlayEvent2D(EventID.MusNour);
                break;

            case CutSceneID.NourPartB:
                break;

            case CutSceneID.BarbrookPartA:
                AudioManager.Instance.PlayEvent2D(EventID.MusBarbrook);
                break;

            case CutSceneID.BarbrookPicNic:
                break;

            case CutSceneID.Bed:
                break;

            case CutSceneID.Drying:
                break;

            case CutSceneID.Plant:
                break;
        }
    }
    
}
