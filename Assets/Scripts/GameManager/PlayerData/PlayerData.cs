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
    public LinkedList<string> dialogLog;
    public PlayerVariable[] variables;
    public List<string> herbarium;
    public List<string> pinnedQuests;
}
