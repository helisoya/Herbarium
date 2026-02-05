using UnityEngine;

public class AudioNour : MonoBehaviour
{
    [SerializeField] EventID NourIdle;
    [SerializeField] EventID NourJump;
    [SerializeField] EventID NourRun;

    public void PostNourIdle()
    {
        AudioManager.Instance.PlayOneShot3D(NourIdle, gameObject);
    }

    public void PostNourJump()
    {
        AudioManager.Instance.PlayOneShot3D(NourJump, gameObject);
    }

    public void PostNourRun()
    {
        AudioManager.Instance.PlayOneShot3D(NourRun, gameObject);
    }

}
