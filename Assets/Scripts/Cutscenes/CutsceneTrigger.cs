using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Executes cutscenes on trigger enter & exit
/// </summary>
public class CutsceneTrigger : MonoBehaviour
{  
    [SerializeField] private DialogGraph dialogGraph;
    [SerializeField] private bool executeOnEnter;
    [SerializeField] private bool executeOnExit;
    [SerializeField] private bool canOverrideCutscenes;

    [Header("Audio")]
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;

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
        if(CutsceneManager.instance.inCutscene && !canOverrideCutscenes) return;

        if (enterTag)
        {
            enterTag = false;

            if (executeOnEnter)
            {
                CutsceneManager.instance.ProcessCutscene(dialogGraph,gameObject);
                onEnter.Invoke();
            }
        }

        if (exitTag)
        {
            exitTag = false;

            if (executeOnExit)
            {
                CutsceneManager.instance.ProcessCutscene(dialogGraph,gameObject);
                onExit.Invoke();
            }   
        }
    }
}