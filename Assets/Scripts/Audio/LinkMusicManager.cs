using UnityEngine;

public class LinkMusicManager : MonoBehaviour
{
    public void PostCutScene(MusicManager.CutSceneID cutSceneID)
    {
        MusicManager.Instance.PostCutScene(cutSceneID);
    }

    public void EndCutScene(MusicManager.CutSceneID cutSceneID)
    {
        MusicManager.Instance.EndCutScene(cutSceneID);
    }
}
