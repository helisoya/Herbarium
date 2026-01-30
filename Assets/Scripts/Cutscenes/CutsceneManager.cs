using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using XNode;

/// <summary>
/// Handles the game's cutscenes
/// </summary>
public class CutsceneManager : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField] private UnityEvent<MusicManager.CutSceneID> onStartCustscene;
    [SerializeField] private UnityEvent<MusicManager.CutSceneID> onEndCustscene;


    public static CutsceneManager instance;
    private Coroutine processingCutscene = null;
    public bool inCutscene {get{return processingCutscene != null;}}
    public bool inParrallelCutscene {get{return currentCutsceneIsParrallel;}}

    private bool userSubmit;
    private bool currentCutsceneIsParrallel = false;

    private Dictionary<string, GameObject> objects;
    private GameObject currentObject;
    private MusicManager.CutSceneID currentCutsceneID;

    /// <summary>
    /// Sets the user submit tag
    /// </summary>
    public void UserSubmit()
    {
        userSubmit = true;
    }

    /// <summary>
    /// Return and consume the user submit tag
    /// </summary>
    /// <returns>True if the submit tag was set</returns>
    public bool ConsumeUserSubmit(){
        bool value = userSubmit;
        userSubmit = false;
        return value;
    }

    /// <summary>
    /// Registers an object
    /// </summary>
    /// <param name="id">Its id</param>
    /// <param name="obj">The object</param>
    public void RegisterObject(string id, GameObject obj)
    {
        objects.TryAdd(id, obj);
    }

    /// <summary>
    /// Gets a registered object in the cutscene system
    /// </summary>
    /// <param name="id">The object's id. "THIS" represents the object that launched the interaction.</param>
    /// <returns>The object</returns>
    public GameObject GetObject(string id)
    {
        if(string.IsNullOrEmpty(id)) return null;
        
        if(id.Equals("THIS") && currentObject != null)
        {
           return currentObject;
        } 
        else
        {
            if (objects.TryGetValue(id, out GameObject obj))
            {
                return obj;
            }
        }
        return null;
    }

    void Awake()
    {
        instance = this;
        currentObject = null;
        objects = new Dictionary<string, GameObject>();
    }

    /// <summary>
    /// Stop processing a cutscene
    /// </summary>
    public void StopProcessing(){
        if(processingCutscene != null){
            StopCoroutine(processingCutscene);
            processingCutscene = null;
        }
    }

    /// <summary>
    /// Start processing a cutscene
    /// </summary>
    /// <param name="graph">The cutscene's graph</param>
    /// <param name="cutsceneID">The audio cutscene ID</param>
    /// <param name="initiatorObject">The initiator object</param>
    /// <param name="overridePreviousCutscene">True if the previous cutscene should be overriden</param>
    /// <param name="playCutscenesEvents">True if the cutscenes audio event should be played</param>
    public void ProcessCutscene(DialogGraph graph,MusicManager.CutSceneID cutsceneID, GameObject initiatorObject, bool overridePreviousCutscene = true, bool playCutscenesEvents = true){
        if (processingCutscene != null && !overridePreviousCutscene) return;

        if(initiatorObject) currentObject = initiatorObject;

        if (processingCutscene != null)
        {
            onEndCustscene.Invoke(currentCutsceneID);
            StopCoroutine(processingCutscene);
        }
        processingCutscene = StartCoroutine(Routine_ProcessingCutscene(graph,cutsceneID,playCutscenesEvents));
    }


    /// <summary>
    /// Routine for processing a dialog graph
    /// </summary>
    /// <param name="graph">The graph</param>
    /// <param name="cutsceneID">The audio cutscene ID</param>
    /// <param name="playCutscenesEvents">True if the cutscenes audio event should be played</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator Routine_ProcessingCutscene(DialogGraph graph, MusicManager.CutSceneID cutsceneID, bool playCutscenesEvents){
        yield return new WaitForEndOfFrame();
        currentCutsceneIsParrallel = graph.parrallelCutscene;
        HerbariumNode currentNode = graph.GetStartNode();
        int result = 0;
        NodePort port;
        currentCutsceneID = cutsceneID;

        if(playCutscenesEvents)onStartCustscene.Invoke(cutsceneID);
        GameGUI.instance.DisableHud();

        while(currentNode != null){
            
            yield return Run<int>(currentNode.Apply(), (output) => result = output);

            // Next node
            if(currentNode.Outputs.Count() > result){
                port = currentNode.Outputs.ElementAt(result);
                if(port.IsConnected) currentNode = (HerbariumNode)port.Connection.node;
                else currentNode = null;
            }else{
                currentNode = null;
            }
        }
        
        Player.instance.ResetCameraTarget();
        if(playCutscenesEvents) onEndCustscene.Invoke(cutsceneID);
        GameGUI.instance.EnableHudIfPossible();

        processingCutscene = null;
        yield return null;
    }


    /// <summary>
    /// Runs a Coroutine with a return value
    /// </summary>
    /// <typeparam name="T">The return value's type</typeparam>
    /// <param name="target">The target Coroutine</param>
    /// <param name="output">The output action</param>
    /// <returns>IEnumerator</returns>
    public static IEnumerator Run<T>(IEnumerator target, Action<T> output)
    {
        object result = null;
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
        output((T)result);
    }
}
