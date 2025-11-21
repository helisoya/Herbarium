using TMPro;
using UnityEngine;

/// <summary>
/// Represents the 
/// </summary>
[CreateAssetMenu(fileName = "LocalsData", menuName = "Herbarium/Locals/LocalsData")]
public class LocalsData : ScriptableObject
{
    public string[] languages;
    public TMP_FontAsset[] fonts;
    public int[] sizes;
}
