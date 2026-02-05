using UnityEngine;

public class AudioPlantsParam : MonoBehaviour
{
    public void PostUnlockPlant(string plantName, float value)
    {
        switch (plantName)
        {
            case "Fern_01":
                AudioManager.Instance.SetGlobalParameterByName("CapillaireInHerbarium", (int)value);
                break;

            case "Herb_02":
                AudioManager.Instance.SetGlobalParameterByName("MentheInHerbarium", (int)value);
                break;

            case "Herb_01":
                AudioManager.Instance.SetGlobalParameterByName("CressonInHerbarium", (int)value);
                break;

        }

    }
}
