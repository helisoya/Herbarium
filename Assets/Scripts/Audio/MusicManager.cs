using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private EventID ExploMusicEvent;
    [SerializeField] private ParamID HerbariumEntries;

    void Start()
    {
        AudioManager.Instance.Play2DEvent(EventID.Enviro2DMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
