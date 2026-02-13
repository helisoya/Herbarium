using UnityEngine;

public class AudioGrassCollision : MonoBehaviour
{
    [SerializeField] EventID grassCollision;

    public void PostGrassCollision(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(grassCollision, obj);
        Debug.Log("Collision Grass");
    }

    public void PostGrassCollision2D()
    {
        AudioManager.Instance.PlayOneShot2D(grassCollision);
        Debug.Log("Collision Grass");
    }
}
