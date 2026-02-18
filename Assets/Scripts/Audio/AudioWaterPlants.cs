using System.Diagnostics.Tracing;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioWaterPlants : MonoBehaviour
{
    [SerializeField] EventReference bigWaterPlant;
    [SerializeField] EventReference lilWaterPlant;

    EventInstance bigWaterPlantInstance;
    EventInstance lilWaterPlantInstance;

    public void OnEnterBigWaterPlant()
    {
        if (bigWaterPlantInstance.isValid())
            return;

        bigWaterPlantInstance = RuntimeManager.CreateInstance(bigWaterPlant);
        RuntimeManager.AttachInstanceToGameObject(bigWaterPlantInstance, gameObject, GetComponent<Rigidbody>());
        bigWaterPlantInstance.start();
    }

    public void OnExitBigWaterPlant()
    {
        if (!bigWaterPlantInstance.isValid())
            return;

        bigWaterPlantInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        bigWaterPlantInstance.release();
    }

    public void OnEnterLilWaterPlant()
    {
        if (lilWaterPlantInstance.isValid())
            return;

        lilWaterPlantInstance = RuntimeManager.CreateInstance(lilWaterPlant);
        RuntimeManager.AttachInstanceToGameObject(lilWaterPlantInstance, gameObject, GetComponent<Rigidbody>());
        lilWaterPlantInstance.start();
    }

    public void OnExitLilWaterPlant()
    {
        if (!lilWaterPlantInstance.isValid())
            return;

        lilWaterPlantInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        lilWaterPlantInstance.release();
    }


    void OnDestroy()
    {
        if (bigWaterPlantInstance.isValid())
        {
            bigWaterPlantInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bigWaterPlantInstance.release();
        }

        if (lilWaterPlantInstance.isValid())
        {
            lilWaterPlantInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            lilWaterPlantInstance.release();
        }
    }
}
