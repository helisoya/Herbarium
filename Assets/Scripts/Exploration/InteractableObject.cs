using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents an interactable object in Spailpin
/// </summary>
public class InteractableObject : MonoBehaviour
{
    [Header("Interaction")]
    public bool stopPlayerOnInterract = true;
    [SerializeField] protected DialogGraph linkedGraph;
    [SerializeField] protected bool playCutscenesEvents = true;
    [SerializeField] protected MusicManager.CutSceneID audioCutsceneId = MusicManager.CutSceneID.Empty;
    [SerializeField] protected string animationTrigger;
    
    protected Renderer[] renderers;
    protected bool playerCouldInteract = false;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        Map.instance.RegisterInteractableObject(this);

        SetHighlight(Settings.instance.GetObjectOutlineActive() ? Settings.instance.GetObjectsOutlineStrength() : 0.0f, Settings.instance.GetObjectsOutlineColor());
    }

    void OnDestroy()
    {
        Map.instance.UnRegisterInteractableObject(this);        
    }

    /// <summary>
    /// Sets the linked dialog graph for this interaction
    /// </summary>
    /// <param name="graph">The dialog graph</param>
    public void SetDialogGraph(DialogGraph graph)
    {
        linkedGraph = graph;
    }

    /// <summary>
    /// Sets the audio cutscene id of the interaction
    /// </summary>
    /// <param name="id">The new id</param>
    public void SetAudioCutsceneId(MusicManager.CutSceneID id)
    {
        audioCutsceneId = id;
    }

    /// <summary>
    /// Sets the highlight for an interactable
    /// </summary>
    /// <param name="strength">The highlight's strength</param>
    /// <param name="color">The highlight's color</param>
    public void SetHighlight(float strength, Color color)
    {
        foreach(Renderer renderer in renderers)
        {
            renderer.material.SetFloat("_HighlightStrength",strength);
            renderer.material.SetColor("_HighlightColor",color);
        }
    }

    /// <summary>
    /// Changes if the interaction is "active" or not
    /// </summary>
    /// <param name="value">True if active</param>
    public void SetActive(bool value)
    {
        playerCouldInteract = value;
    }
    
    /// <summary>
    /// Gets the animation trigger linked to the interaction
    /// </summary>
    /// <returns>The animation trigger</returns>
    public string GetAnimationTrigger()
    {
        return animationTrigger;
    }

    /// <summary>
    /// Interacts with the object
    /// </summary>
    public void Interract()
    {
        print("Interaction with : " + this.name);
        OnInterract();
    }

    /// <summary>
    /// Callback on interraction
    /// </summary>
    protected virtual void OnInterract()
    {
        // Do thing with the graph
        CutsceneManager.instance.ProcessCutscene(linkedGraph, audioCutsceneId, gameObject,true,playCutscenesEvents);
    }


    void Update()
    {
        OnUpdate();
    }

    /// <summary>
    /// Called on Update
    /// </summary>
    public virtual void OnUpdate()
    {

    }

}
