using UnityEngine;

public class AudioNour : MonoBehaviour
{
    [SerializeField] EventID NourIdle;
    [SerializeField] EventID NourJump;
    [SerializeField] EventID NourRun;

    public void PostNourIdle(GameObject nourPos)
    {
        AudioManager.Instance.PlayOneShot3D(NourIdle, nourPos);
    }

    public void PostNourJump(GameObject nourPos)
    {
        AudioManager.Instance.PlayOneShot3D(NourJump, nourPos);
    }

    public void PostNourRun(GameObject nourPos)
    {
        AudioManager.Instance.PlayOneShot3D(NourRun, nourPos);
    }

}
