using UnityEngine;

public class AudioCollision : MonoBehaviour
{
    [SerializeField] private EventID treeCollision;
    [SerializeField] private EventID plantCollision;
    [SerializeField] private EventID rockCollision;
    [SerializeField] private EventID npcCollision;


    public void PostTreeCollision(GameObject obj)
    {
        
        AudioManager.Instance.Play3DEvent(treeCollision, obj);
        Debug.Log("AIE L'ARBRE IMPACT");
    }

    public void PostPlantCollision(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(plantCollision, obj);
    }
   
}
