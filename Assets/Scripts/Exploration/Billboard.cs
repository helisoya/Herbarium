using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera cam;
    
    void Update()
    {
        if (cam != null)
            transform.rotation= cam.transform.rotation;
    }
}
