using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioPlayerAniù : MonoBehaviour
{
    [SerializeField] private EventReference playerWalk;
    //[SerializeField] EventID playerBend;
    //[SerializeField] EventID playerInteract;
    [SerializeField] EventReference playerCloatUp;
    [SerializeField] EventReference playerCloatDown;
    
    private EventInstance playerWalkInstance;
    private EventInstance playerCloatUpInstance;
    private EventInstance playerCloatDownInstance;

    public void PostPlayerWalk()
    {
        playerWalkInstance = RuntimeManager.CreateInstance(playerWalk);
        playerWalkInstance.start();
    }

    public void PostPlayerCloatDown()
    {
        playerCloatDownInstance = RuntimeManager.CreateInstance(playerCloatDown);
        playerCloatDownInstance.start();
    }

    public void PostPlayerCloatUp()
    {
        playerCloatUpInstance = RuntimeManager.CreateInstance(playerCloatUp);
        playerCloatUpInstance.start();
    }

    public void PostPlayerIdle()
    {
        playerWalkInstance.stop(STOP_MODE.IMMEDIATE);
        playerCloatUpInstance.stop(STOP_MODE.IMMEDIATE);
        playerCloatDownInstance.stop(STOP_MODE.IMMEDIATE);
    }
}
