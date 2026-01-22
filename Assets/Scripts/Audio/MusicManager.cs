using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private EventID AmbEvent;
    [SerializeField] private EventID ExploMusicEvent;
    [SerializeField] private ParamID HerbariumEntries;

    void Start()
    {
        AudioManager.Instance.PlayEvent2D(AmbEvent);
        AudioManager.Instance.PlayEvent2D(ExploMusicEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
