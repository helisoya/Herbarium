using UnityEngine;
using UnityEngine.Events;

public class ProximityTrigger : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            onEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            onExit.Invoke();
        }
    }
}
