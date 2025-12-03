using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Execute events on collision enter & exit
/// </summary>
public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private bool enterTag;
    private bool exitTag;

    public void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Player")
        {
            enterTag = true;
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if(other.transform.tag == "Player")
        {
           exitTag = true;
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
