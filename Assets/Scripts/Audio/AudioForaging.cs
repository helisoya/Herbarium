using FMOD.Studio;
using UnityEngine;

public class AudioForaging : MonoBehaviour
{
    [SerializeField] private EventID startForaging;
    [SerializeField] private EventID stopForagingGood;
    [SerializeField] private EventID stopForagingBad;
    [SerializeField] private EventID stopForagingCancel;
    [SerializeField] private EventID cut;
    [SerializeField] private EventID cutGood;
    [SerializeField] private EventID cutBad;
    [SerializeField] private EventID toolPickUp;
    [SerializeField] private EventID toolDrop;
    [SerializeField] private EventID toolMove;
    [SerializeField] private EventID toolStop;
    [SerializeField] private EventID toolImpact;
    [SerializeField] private EventID plantPickUp;
    [SerializeField] private EventID plantDrop;
    [SerializeField] private EventID plantMove;
    [SerializeField] private EventID plantStop;
    [SerializeField] private EventID plantImpact;
    [SerializeField] private EventID plantStress;
    [SerializeField] private EventID stopPlantStress;
    [SerializeField] private EventID inBag;
    

    private GameObject currentMovingObject;
    private EventInstance plantStressInstance;

    /// <summary>
    /// PARAMETERS
    /// </summary>
    /// <param name="value"></param>
    public void OnChangePlant(string plant)
    {
        switch (plant)
        {
            case "Fern_01":
                AudioManager.Instance.SetGlobalParameterByName("Plant", 0);
                Debug.Log("C'est la capillaire");
                break;
            case "Herb_01":
                AudioManager.Instance.SetGlobalParameterByName("Plant", 2);
                Debug.Log("C'est le cresson");
                break;
            case "Herb_02":
                AudioManager.Instance.SetGlobalParameterByName("Plant", 1);
                Debug.Log("C'est la menthe");
                break;

        }
        ;
    }



    public void PostStartForaging()
    {
        AudioManager.Instance.PlayOneShot2D(startForaging);
        Debug.Log("ça va trancher");
    }

    public void PostStopForaging(MicroInteraction.EndingType endingType)
    {
        switch (endingType)
        {
            case MicroInteraction.EndingType.SUCCESS:
                AudioManager.Instance.PlayOneShot2D(stopForagingGood);
                break;
            case MicroInteraction.EndingType.FAILURE:
                AudioManager.Instance.PlayOneShot2D(stopForagingBad);
                break;
            case MicroInteraction.EndingType.CANCEL:
                AudioManager.Instance.PlayOneShot2D(stopForagingCancel);
                break;

        }
    }

    public void PostCut()
    {
        AudioManager.Instance.PlayOneShot3D(cut, currentMovingObject);
    }

    public void PostCutGood()
    {
        AudioManager.Instance.PlayOneShot3D(cutGood, currentMovingObject);
    }

    public void PostCutBad()
    {
        AudioManager.Instance.PlayOneShot3D(cutBad, currentMovingObject);
    }

    public void PostToolImpact(Transform obj)
    {
        AudioManager.Instance.PlayOneShot3D(toolImpact, obj.gameObject);
    }

    public void PostPlantImpact(Transform obj)
    {
        AudioManager.Instance.PlayOneShot3D(plantImpact, obj.gameObject);
    }

    public void PostPickUp(MicroInteraction.PickupAudioData data)
    {
        currentMovingObject = data.movingObject;
        if (data.type == MicroInteractionPickable.PickableType.CUTTER)
        {
            AudioManager.Instance.PlayOneShot3D(toolPickUp, currentMovingObject);
        }

        if (data.type == MicroInteractionPickable.PickableType.PLANT)
        {
            AudioManager.Instance.PlayOneShot3D(plantPickUp, currentMovingObject);
        }
    }

    public void PostDrop(MicroInteraction.PickupAudioData data)
    {
        if (data.type == MicroInteractionPickable.PickableType.CUTTER)
        {
            AudioManager.Instance.PlayOneShot3D(toolDrop, currentMovingObject);
        }

        if (data.type == MicroInteractionPickable.PickableType.PLANT)
        {
            AudioManager.Instance.PlayOneShot3D(plantDrop, currentMovingObject);
        }
        currentMovingObject = null;
    }

    public void PostPlantStress()
    {
        Debug.Log("je stresse la plante");
        plantStressInstance = AudioManager.Instance.PlayEvent3D(plantStress, currentMovingObject);
        //AudioManager.Instance.PlayOneShot3D(plantImpact, currentMovingObject);
    }

    public void StopPlantStress()
    {
        Debug.Log("ok d'accord j'arr�te de stresser la plante");
        AudioManager.Instance.Stop(plantStressInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StopPlantStressEvent()
    {
        Debug.Log("ok d'accord j'arr�te de stresser la plante event");
        AudioManager.Instance.PlayOneShot2D(stopPlantStress);
    }
    public void PostInBag()
    {
        AudioManager.Instance.PlayOneShot2D(inBag);
        Debug.Log("la main dans le sac !!!");
    }

}
