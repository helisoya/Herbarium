using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using AYellowpaper.SerializedCollections;

/// <summary>
/// Handles the game's input icons
/// </summary>
public class InputIcons : MonoBehaviour
{
    public static InputIcons instance;
    public SerializedDictionary<string, Sprite> icons;
    public Sprite defaultIcon;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Gets an input's icon
    /// </summary>
    /// <param name="deviceLayoutName">The device name</param>
    /// <param name="controlPath">The input's path</param>
    /// <returns>The sprite if it exists</returns>
    public Sprite GetIcon(string deviceLayoutName, string controlPath){
        //if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "DualShockGamepad"))
        //    icon = ps4.GetSprite(controlPath);
        if (icons.TryGetValue(controlPath, out Sprite icon)) return icon;

        return defaultIcon;
    }
}

