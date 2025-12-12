using System;
using UnityEngine;

/// <summary>
/// Represents the data of a radial menu
/// </summary>
public struct RadialMenuData
{
    public RadialMenuEntryData[] entries;
    public float radius;

}

/// <summary>
/// Represents the data of a radial menu entry
/// </summary>
public struct RadialMenuEntryData
{
    public Sprite sprite;
    public string key;
    public bool interactable;
    public object[] injectors;
    public Action callback;
}