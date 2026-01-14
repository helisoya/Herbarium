using UnityEngine;

public class AudioCollision : MonoBehaviour
{
    [SerializeField] private EventID treeCollision;
    [SerializeField] private EventID plantCollision;
    [SerializeField] private EventID rockCollision;
    [SerializeField] private EventID grassCollision;
    [SerializeField] private EventID npcCollision;


    public void PostTreeCollision(GameObject obj)
    {
        
        AudioManager.Instance.PlayOneShot3D(treeCollision, obj);
        Debug.Log("AIE L'ARBRE IMPACT");
    }

    public void PostPlantCollision(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(plantCollision, obj);
        Debug.Log("AIE LA PLANTE IMPACT");
    }

    public void PostRockCollision(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(rockCollision, obj);
        Debug.Log("AIE LE ROCHER IMPACT");
    }

    public void PostGrassCollision(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(grassCollision, obj);
        Debug.Log("je marche sur l'herbe oui oui");
    }

    public void PostNpcCollision(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(npcCollision, obj);
        Debug.Log("AIE LE NPC IMPACT");
    }
}
