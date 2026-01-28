using System;
using UnityEngine;


/// <summary>
/// Represents a quest
/// </summary>
[System.Serializable]
public class Quest
{
    public string id;
    public string linkedVariable;
    public Sprite questSprite;


    public string Name {get{return id + "_Name";}}
    public string Description {get{return id+"_Description";}}
    public string Lore {get{return id+"_Lore";}}

    /// <summary>
    /// Gets the state of the quest
    /// </summary>
    /// <param name="progress">The quest progression</param>
    /// <returns>The state</returns>
    public string GetState(int progress)
    {
        return id+"_"+progress;
    }
}
