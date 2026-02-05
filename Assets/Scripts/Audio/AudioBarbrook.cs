using UnityEngine;

public class AudioBarbrook : MonoBehaviour
{
    [SerializeField] EventID BarbrookSleeping;
    [SerializeField] EventID BarbrookWakeUp;
    [SerializeField] EventID BarbrookIdle;
    [SerializeField] EventID BarbrookHappy;

    public void PostBarbrookSleeping()
    {
        AudioManager.Instance.PlayOneShot3D(BarbrookSleeping, gameObject);
    }

    public void PostBarbrookWakeUp()
    {
        AudioManager.Instance.PlayOneShot3D(BarbrookWakeUp, gameObject);
    }

    public void PostBarbrookIdle()
    {
        AudioManager.Instance.PlayOneShot3D(BarbrookIdle, gameObject);
    }

    public void PostBarbrookHappy()
    {
        AudioManager.Instance.PlayOneShot3D(BarbrookHappy, gameObject);
    }
}
