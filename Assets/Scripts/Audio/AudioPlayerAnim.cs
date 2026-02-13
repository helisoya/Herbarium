using UnityEngine;

public class AudioPlayerAniù : MonoBehaviour
{
    [SerializeField] EventID playerWalk;
    //[SerializeField] EventID playerWalkStop;
    //[SerializeField] EventID playerBend;
    [SerializeField] EventID playerInteract;
    [SerializeField] EventID playerCloatUp;
    [SerializeField] EventID playerCloatDown;

    public void PostPlayerWalk()
    {
        AudioManager.Instance.PlayOneShot2D(playerWalk);
    }

    public void PostPlayerCloatDown()
    {
        AudioManager.Instance.PlayOneShot2D(playerCloatDown);
    }

    public void PostPlayerCloatUp()
    {
        AudioManager.Instance.PlayOneShot2D(playerCloatUp);
    }

    public void PostPlayerInteract()
    {
        AudioManager.Instance.PlayOneShot2D(playerInteract);
    }
}
