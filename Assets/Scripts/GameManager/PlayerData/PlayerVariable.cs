using System;
using UnityEngine;

/// <summary>
/// Represents a variable that needs to be saved. It is used amon other in the dialog graph
/// (Quests, events, ....)
/// </summary>
[System.Serializable]
public struct PlayerVariable
{
    public string id;
    public int value;
}
