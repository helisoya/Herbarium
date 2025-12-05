using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Executes events on trigger enter & exit
/// </summary>
public class ProximityTrigger : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private bool enterTag;
    private bool exitTag;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enterTag = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            exitTag = true;
            onExit.Invoke();
        }
    }

    void Update()
    {
        if (enterTag)
        {
            enterTag = false;
            onEnter.Invoke();
        }

        if (exitTag)
        {
            exitTag = false;
            onExit.Invoke();
        }
    }
}
