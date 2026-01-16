using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents a channel's saveable data for the localization system
/// A channel represents a set of size, color and font for multiple texts
/// </summary>
[System.Serializable]
public class LocalChannelData
{
    public int sizeIndex;
    public int fontIndex;
    public Color color;
}

/// <summary>
/// Represents a channel for the localization system
/// It is a combination of the channel's data and the unityevents linked to it
/// </summary>
public class LocalChannel
{
    public LocalChannelData data;
    public UnityEvent<TMP_FontAsset> onChangeFont;
    public UnityEvent<int> onChangeSize;
    public UnityEvent<Color> onChangeColor;
}
