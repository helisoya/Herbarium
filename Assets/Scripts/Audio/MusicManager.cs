using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private EventID AmbEvent;
    [SerializeField] private EventID ExploMusicEvent;
    [SerializeField] private ParamID HerbariumEntries;

    void Start()
    {
        AudioManager.Instance.Play2DEvent(EventID.Enviro2DMusic);
        AudioManager.Instance.Play2DEvent(EventID.Amb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
