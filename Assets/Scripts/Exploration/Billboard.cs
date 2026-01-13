using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera cam;
    
    void Update()
    {
        transform.rotation= cam.transform.rotation;
    }
}
