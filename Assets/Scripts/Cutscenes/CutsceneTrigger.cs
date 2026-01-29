using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Executes cutscenes on trigger enter & exit
/// </summary>
public class CutsceneTrigger : MonoBehaviour
{  
    [Header("Graph")]
    [SerializeField] private DialogGraph dialogGraph;
    [SerializeField] private MusicManager.CutSceneID audioCutsceneId;
    [SerializeField] private bool canOverrideCutscenes;

    [Header("Trigger")]
    [SerializeField] private bool executeOnEnter;
    [SerializeField] private bool executeOnExit;
    [SerializeField] private bool executeOnlyOnce;
    private bool active = true;


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
        if((CutsceneManager.instance.inCutscene && !canOverrideCutscenes) || !active) return;

        if (enterTag)
        {
            enterTag = false;

            if (executeOnEnter)
            {
                CutsceneManager.instance.ProcessCutscene(dialogGraph,audioCutsceneId,gameObject);
                onEnter.Invoke();

                if(executeOnlyOnce) active = false;
            }
        }

        if (exitTag)
        {
            exitTag = false;

            if (executeOnExit)
            {
                CutsceneManager.instance.ProcessCutscene(dialogGraph,audioCutsceneId,gameObject);
                onExit.Invoke();

                if(executeOnlyOnce) active = false;
            }   
        }
    }
}