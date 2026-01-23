using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the player data
/// Inventory, position in world, herbarium, ...
/// </summary>
[System.Serializable]
public class PlayerData
{
    public string[] inventory;
    public List<DialogLog> dialogLog;
    public PlayerVariable[] variables;
    public List<string> herbarium;
    public List<string> pinnedQuests;
    public bool hadForagingTutorial = false;
    public List<RegrowthPlantData> regrowthData;
    public string mapName;
    public Vector3 mapPosition;
}


/// <summary>
/// Represents a link between an Id and a regrowth time
/// Used to represent the current regrowth timer for an individual plant
/// Are you confused yet ? Good !
/// </summary>
[System.Serializable]
public class RegrowthPlantData{
    public string entityId;
    public int regrowthTime;
}

/// <summary>
/// Represent a dialog log
/// </summary>
[System.Serializable]
public class DialogLog
{
    public string speakerId;
    public string dialogId;
}