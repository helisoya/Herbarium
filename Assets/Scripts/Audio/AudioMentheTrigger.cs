using UnityEngine;

public class AudioMentheTrigger : MonoBehaviour
{
    
    public void OnMentheEnter()
    {
        AudioManager.Instance.PlayOneShot3D(EventID.MusExploration3DMenthe, gameObject);
    }
}
