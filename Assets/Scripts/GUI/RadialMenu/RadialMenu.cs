using System;
using UnityEngine;

/// <summary>
/// Represents a radial menu
/// </summary>
public class RadialMenu : MonoBehaviour
{

    [SerializeField] private Transform radialParent;
    [SerializeField] private RadialMenuEntry entryPrefab;
    [SerializeField] private RadialMenuEntry backEntry;

    public bool inRadialMenu {get; private set;}
    private RadialMenuEntry[] entries;
    private RadialMenuData currentData;

    void Awake()
    {
        backEntry.SetCanBeInteractedWith(false);
        backEntry.SetScale(Vector3.zero,true);
    }


    /// <summary>
    /// Open the radial menu
    /// </summary>
    /// <param name="data">The radial menu's data</param>
    public void Open(RadialMenuData data)
    {
        Cleanup();
        currentData = data;

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

        backEntry.SetCanBeInteractedWith(true);
        backEntry.SetCallback(currentData.backCallback);
        backEntry.SetScale(Vector3.one, false);
    }



    /// <summary>
    /// Closes the radial menu
    /// </summary>
    public void Close()
    {
        Cleanup();
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

        backEntry.SetScale(Vector3.zero, false);
        backEntry.SetCanBeInteractedWith(false);
    }


    void OpenDebug()
    {
        RadialMenuData testData = new RadialMenuData();
        testData.radius = 100f;
        testData.entries = new RadialMenuEntryData[5];
        testData.backCallback = Close;
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
            key = "test_3",
            sprite = null,
            callback = OpenDebug,
            interactable = true
        };
        testData.entries[3] = new RadialMenuEntryData()
        {
            key = "test_4",
            sprite = null,
            callback = OpenDebug,
            interactable = false
        };
        testData.entries[4] = new RadialMenuEntryData()
        {
            key = "test_5",
            sprite = null,
            callback = OpenDebug,
            interactable = false
        };

        Open(testData);
    }
}
