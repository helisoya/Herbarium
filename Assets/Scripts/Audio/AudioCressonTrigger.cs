using UnityEngine;

public class AudioCressonTrigger : MonoBehaviour
{
    
    public void OnCressonEnter()
    {
        AudioManager.Instance.PlayOneShot3D(EventID.MusExploration3DCresson, gameObject);
    }
}
