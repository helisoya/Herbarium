using UnityEngine;

/// <summary>
/// Marker for aligning sprites
/// (Does nothing, check editor instead)
/// </summary>
public class AlignSprites : MonoBehaviour
{
    public Sprite[] sprites;
    [Range(0f, 5f)]
    public float slider;

    void Awake()
    {
        Destroy(this);
    }
}
