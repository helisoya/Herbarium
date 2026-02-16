using UnityEngine;

public class AudioBarbrookZone : MonoBehaviour
{
    public void OnBarbrookZoneEnter()
    {
        AudioManager.Instance.SetGlobalParameterByName("Zone", 2);
    }

    public void OnBarbrookZoneExit()
    {
        AudioManager.Instance.SetGlobalParameterByName("Zone", 1);
    }
}
