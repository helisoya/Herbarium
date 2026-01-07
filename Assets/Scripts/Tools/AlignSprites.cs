using UnityEngine;

/// <summary>
/// Marker for aligning sprites
/// (Does nothing, check editor instead)
/// </summary>
public class AlignSprites : MonoBehaviour
{
    public Sprite[] sprites;
    [Range(0f, 5f)] public float displacement = 0.1f;
    [Range(0f, 360f)] public float rotation = 45f;

    void Awake()
    {
        Destroy(this);
    }
}
