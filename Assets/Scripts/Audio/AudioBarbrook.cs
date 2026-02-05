using UnityEngine;

public class AudioBarbrook : MonoBehaviour
{
    [SerializeField] EventID BarbrookSleeping;
    [SerializeField] EventID BarbrookWakeUp;
    [SerializeField] EventID BarbrookIdle;
    [SerializeField] EventID BarbrookHappy;

    public void PostBarbrookSleeping(GameObject barbrookPos)
    {
        AudioManager.Instance.PlayOneShot3D(BarbrookSleeping, barbrookPos);
    }

    public void PostBarbrookWakeUp(GameObject barbrookPos)
    {
        AudioManager.Instance.PlayOneShot3D(BarbrookWakeUp, barbrookPos);
    }

    public void PostBarbrookIdle(GameObject barbrookPos)
    {
        AudioManager.Instance.PlayOneShot3D(BarbrookIdle, barbrookPos);
    }

    public void PostBarbrookHappy(GameObject barbrookPos)
    {
        AudioManager.Instance.PlayOneShot3D(BarbrookHappy, barbrookPos);
    }
}
