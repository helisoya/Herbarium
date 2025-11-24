using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a dialog graph in Herbarium
/// </summary>
[CreateAssetMenu(menuName ="Herbarium/DialogGraph")]
public class DialogGraph : NodeGraph { 
    public bool parrallelCutscene = false;

    /// <summary>
    /// Gets the starting node from the graph
    /// </summary>
    /// <returns>The starting node if it exists</returns>
    public HerbariumNode GetStartNode(){
        foreach(HerbariumNode node in nodes){
            if(node.GetType() == typeof(StartNode)){ return node;}
        }
        return null;
    }
}