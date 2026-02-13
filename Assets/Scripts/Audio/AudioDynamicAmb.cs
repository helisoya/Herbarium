using UnityEngine;

public class AudioDynamicAmb : MonoBehaviour
{

    public void OnSecretZoneEnter()
    {
        AudioManager.Instance.SetGlobalParameterByName("SecretZone", 1);
    }

    public void OnSecretZoneExit()
    {
        AudioManager.Instance.SetGlobalParameterByName("SecretZone", 0);
    }
}
