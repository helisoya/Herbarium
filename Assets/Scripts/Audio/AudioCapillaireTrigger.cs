using UnityEngine;

public class AudioCapillaireTrigger : MonoBehaviour
{
    public void OnCapillaireEnter()
    {
        AudioManager.Instance.PlayOneShot3D(EventID.MusExploration3DCapillaire, gameObject);
    }
}
