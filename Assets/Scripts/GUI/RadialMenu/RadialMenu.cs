using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a radial menu
/// </summary>
public class RadialMenu : MonoBehaviour
{

    [SerializeField] private Transform radialParent;
    [SerializeField] private RadialMenuEntry entryPrefab;

    public bool inRadialMenu {get; private set;}
    private RadialMenuEntry[] entries;
    private RadialMenuData currentData;
    private int currentEntryIdx;


    void Start()
    {
        OpenDebug();
    }


    /// <summary>
    /// Open the radial menu
    /// </summary>
    /// <param name="data">The radial menu's data</param>
    public void Open(RadialMenuData data)
    {
        Cleanup();
        currentData = data;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width/2,Screen.height/2));
        currentEntryIdx = -1;

        Invoke("Populate",inRadialMenu ? 0.4f : 0f);
    }

    /// <summary>
    /// Populates the radial menu
    /// </summary>
    public void Populate()
    {   
        inRadialMenu = true;

        entries = new RadialMenuEntry[currentData.entries.Length];
        RadialMenuEntry entry;

        float radiansSeparation = Mathf.PI * 2 / entries.Length;

        for(int i = 0; i < currentData.entries.Length; i++)
        {
            entry = Instantiate(entryPrefab,radialParent);
            entry.Init(currentData.entries[i]);

            entry.SetScale(Vector3.zero, true);
            entry.SetScale(Vector3.one, false);

            entry.SetPosition(0,0,true);
            entry.SetPosition(Mathf.Sin(radiansSeparation * i) * currentData.radius,
                Mathf.Cos(radiansSeparation * i) * currentData.radius,
                false);

            entries[i] = entry;
        }

    }



    /// <summary>
    /// Closes the radial menu
    /// </summary>
    public void Close()
    {
        Cleanup();
        currentEntryIdx = -1;
        inRadialMenu = false;
    }

    /// <summary>
    /// Cleans up the radial menu
    /// </summary>
    private void Cleanup()
    {
        if(entries != null)
        {
            for(int i = 0; i < entries.Length; i++)
            {
                RadialMenuEntry entry = entries[i];
                entry.SetPosition(0,0,false);
                entry.SetScale(Vector3.zero,false);
                entry.SetCanBeInteractedWith(false);
                Destroy(entries[i].gameObject,0.4f);
            }
            entries = null;
        }
    }


    /// <summary>
    /// Updates the mouse position
    /// </summary>
    /// <param name="position">The new position</param>
    public void UpdateMousePosition(Vector2 position)
    {
        if (inRadialMenu && entries != null)
        {
            Vector2 mouseDir = (position - new Vector2(Screen.width/2,Screen.height/2)).normalized;
            float mouseAngle = Mathf.Atan2(mouseDir.x,mouseDir.y);
            if(mouseAngle < 0) mouseAngle += 2 * Mathf.PI;

            float radiansSeparation = Mathf.PI * 2 / entries.Length;
            float value = mouseAngle / radiansSeparation;
            int correctBox = Mathf.FloorToInt(value);

            if(value % 1 >= 0.5f) correctBox = (correctBox + 1) % entries.Length;

            if(currentEntryIdx != correctBox)
            {
                if(currentEntryIdx != -1) entries[currentEntryIdx].StopHighlight();
                currentEntryIdx = correctBox;
                entries[currentEntryIdx].Highlight();
            }

        }
    }

    /// <summary>
    /// Activate the currently selected entry (if any are selected)
    /// </summary>
    public void ActivateCurrentlySelected()
    {
        if(inRadialMenu && currentEntryIdx != -1)
        {
            entries[currentEntryIdx].Activate();
        }
    }

    void OpenDebug()
    {
        RadialMenuData testData = new RadialMenuData();
        testData.radius = 100f;
        testData.entries = new RadialMenuEntryData[4];
        testData.entries[0] = new RadialMenuEntryData()
        {
            key = "test_1",
            sprite = null,
            callback = OpenDebug,
            interactable = true
        };
        testData.entries[1] = new RadialMenuEntryData()
        {
            key = "test_2",
            sprite = null,
            callback = OpenDebug,
            interactable = true
        };
        testData.entries[2] = new RadialMenuEntryData()
        {
            key = "Close",
            sprite = null,
            callback = Close,
            interactable = true
        };
        testData.entries[3] = new RadialMenuEntryData()
        {
            key = "test_4",
            sprite = null,
            callback = OpenDebug,
            interactable = true
        };

        Open(testData);
    }
}
