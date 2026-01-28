using AYellowpaper.SerializedCollections;
using UnityEngine;

/// <summary>
/// Handles the cursor in the game
/// </summary>
public class HerbariumCursor : MonoBehaviour
{
    public enum CursorType
    {
        NORMAL,
        MOVING,
        FORAGENORMAL,
        FORAGEHOLD
    }
    [SerializeField] private SerializedDictionary<CursorType,Texture2D> textures;
    private CursorType currentCursor;

    void Awake()
    {
        currentCursor = CursorType.NORMAL;
        Cursor.SetCursor(textures[CursorType.NORMAL],Vector2.zero,CursorMode.Auto);
    }

    /// <summary>
    /// Changes the cursor
    /// </summary>
    /// <param name="newCursor">The new cursor</param>
    public void ChangeCursor(CursorType newCursor)
    {
        if(currentCursor != newCursor)
        {
            currentCursor = newCursor;
            Cursor.SetCursor(textures[currentCursor],Vector2.zero,CursorMode.Auto);
        }
    }
}
