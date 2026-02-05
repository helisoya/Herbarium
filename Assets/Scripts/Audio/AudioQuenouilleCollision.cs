using UnityEngine;

public class AudioQuenouilleCollision : MonoBehaviour
{

    [SerializeField] EventID quenouilleCollision;

    public void PostGrassCollision(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(quenouilleCollision, obj);
    }
   
}
