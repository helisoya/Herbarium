using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a radial menu
/// </summary>
public class RadialMenu : MonoBehaviour
{
    [Header("Radial Menu")]
    [SerializeField] private Transform radialParent;
    [SerializeField] private RadialMenuEntry entryPrefab;
    [SerializeField] private Sprite backSprite;
    public RadialMenuID currentRadialMenu {get; private set;}
    private RadialMenuEntry[] entries;
    private RadialMenuData currentData;
    private int currentEntryIdx;
    public int selectedIndex {get; private set;}

    [Header("Inputs")]
    [SerializeField] private InputData closeInput;
    [SerializeField] private InputData mapInput;
    [SerializeField] private InputData inventoryInput;
    [SerializeField] private InputData herbariumInput;


    [Header("Audio Event")]
    [SerializeField] private UnityEvent onOpenBackpack;
    [SerializeField] private UnityEvent onCloseBackpack;
    [SerializeField] private UnityEvent onOpenInventory;
    [SerializeField] private UnityEvent onCloseInventory;
    [SerializeField] private UnityEvent onRadialMenuHover;
    [SerializeField] private UnityEvent onRadialMenuClick;

    private const float SIZE = 75f;

    private readonly Vector3[] inputPositions  =
    {
      new Vector3(0,-68,0),
      new Vector3(-68,0,0),
      new Vector3(0,72,0),
      new Vector3(68,0,0)
    };

    [System.Serializable]
    public struct InputData
    {
        public InputActionReference action;
        public int index;
    }

    void Awake()
    {
        currentRadialMenu = RadialMenuID.CLOSED;
    }

    /// <summary>
    /// Open the radial menu
    /// </summary>
    /// <param name="data">The radial menu's data</param>
    public void Open(RadialMenuData data)
    {
        selectedIndex = 0;
        Cleanup();
        currentData = data;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        currentEntryIdx = -1;

        Invoke("Populate", currentRadialMenu != RadialMenuID.CLOSED ? 0.4f : 0f);
    }

    /// <summary>
    /// Populates the radial menu
    /// </summary>
    public void Populate()
    {
        currentRadialMenu = currentData.id;
        entries = new RadialMenuEntry[currentData.entries.Length];
        RadialMenuEntry entry;

        float radiansSeparation = Mathf.PI * 2 / entries.Length;

        for (int i = 0; i < currentData.entries.Length; i++)
        {
            entry = Instantiate(entryPrefab, radialParent);
            entry.Init(currentData.entries[i]);

            entry.SetScale(Vector3.zero, true);
            entry.SetScale(Vector3.one, false);
            entry.SetLabelScale(Vector3.one,true);

            entry.SetPosition(0, 0, true);
            entry.SetPosition(Mathf.Sin(Mathf.PI + radiansSeparation * i) * currentData.radius,
                Mathf.Cos(Mathf.PI + radiansSeparation * i) * currentData.radius,
                false);

            entries[i] = entry;
        }
    }



    /// <summary>
    /// Closes the radial menu
    /// </summary>
    /// <param name="playSound">True if the closing sound should be played</param>
    public void Close(bool playSound = true)
    {
        if (playSound)
        {
            if(currentRadialMenu == RadialMenuID.BACKPACK) onCloseBackpack.Invoke();
            else if (currentRadialMenu == RadialMenuID.INVENTORY) onCloseInventory.Invoke();   
        }

        Cleanup();
        currentRadialMenu = RadialMenuID.CLOSED;
        currentEntryIdx = -1;
    }

    /// <summary>
    /// Cleans up the radial menu
    /// </summary>
    private void Cleanup()
    {
        if (entries != null)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                RadialMenuEntry entry = entries[i];
                entry.SetPosition(0, 0, false);
                entry.SetScale(Vector3.zero, false);
                entry.SetCanBeInteractedWith(false);
                Destroy(entries[i].gameObject, 0.4f);
            }
            entries = null;
        }
    }


    /// <summary>
    /// Updates the mouse position
    /// </summary>
    /// <param name="position">The new position</param>
    /// <param name="forceInteraction">True if the movement should also be counted as an interaction</param>
    public void UpdateMousePosition(Vector2 position, bool forceInteraction = false)
    {
        if (currentRadialMenu != RadialMenuID.CLOSED && entries != null)
        {
            Vector2 mouseDir = (position - new Vector2(Screen.width / 2, Screen.height / 2)).normalized;

            Vector2 correctPos = new Vector2(position.x / Screen.width, position.y / Screen.height);
            Vector2 distToWidth = new Vector2(Mathf.Abs(correctPos.x-0.5f), Mathf.Abs(correctPos.y-0.5f));

            //print(distToWidth+ " "+ SIZE*2f/800f + " "+ SIZE*2f/Screen.height);

            float mouseAngle = Mathf.Atan2(-mouseDir.x, -mouseDir.y);
            if (mouseAngle < 0) mouseAngle += 2 * Mathf.PI;

            float radiansSeparation = Mathf.PI * 2 / entries.Length;
            float value = mouseAngle / radiansSeparation;
            int correctBox = Mathf.FloorToInt(value);

            if (value % 1 >= 0.5f) correctBox = (correctBox + 1) % entries.Length;

            if(distToWidth.x > 0.18f || distToWidth.y > 0.34f) correctBox = -1;

            if (currentEntryIdx != correctBox)
            {
                if (currentEntryIdx != -1) entries[currentEntryIdx].StopHighlight();
                currentEntryIdx = correctBox;

                if(correctBox != -1)
                {
                    entries[currentEntryIdx].Highlight();

                    if (entries[currentEntryIdx].CanBeInteractedWith())
                    {
                        onRadialMenuHover.Invoke();

                        if(forceInteraction) ActivateCurrentlySelected();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Activate the currently selected entry (if any are selected)
    /// </summary>
    public void ActivateCurrentlySelected()
    {
        if (currentRadialMenu != RadialMenuID.CLOSED && currentEntryIdx != -1)
        {
            selectedIndex = currentEntryIdx;
            if (entries[currentEntryIdx].Activate())
            {
                onRadialMenuClick.Invoke();
            }
        }
    }

    private void OpenHerbarium()
    {
        Close(false);
        GameGUI.instance.OpenHerbarium();
    }

    private void CloseBackpack()
    {
        GameGUI.instance.EnableHudIfPossible();
        Close(true);
    }

    private void CloseInventory()
    {
        onCloseInventory.Invoke();
        OpenBackpack(false);
    }


    /// <summary>
	/// Opens the default backpack menu
	/// </summary>
    /// <param name="openSound">True if the opening sound should be invoked</param>
    public void OpenBackpack(bool openSound = true)
    {
        PlayerDataHandler dataHandler = GameManager.instance.GetPlayerDataHandler();

        RadialMenuData testData = new RadialMenuData();
        testData.radius = SIZE;
        testData.id = RadialMenuID.BACKPACK;
        testData.entries = new RadialMenuEntryData[4];
        testData.entries[0] = new RadialMenuEntryData()
        {
            key = "RadialMenu_Close",
            sprite = backSprite,
            rotation = 0,
            callback = CloseBackpack,
            interactable = true,
            inputAction = closeInput.action,
            inputIndex = closeInput.index,
            inputPosition = inputPositions[0]
        };
        testData.entries[2] = new RadialMenuEntryData()
        {
            key = "RadialMenu_Herbarium",
            sprite = backSprite,
            rotation = 180,
            callback = OpenHerbarium,
            interactable = true,
            inputAction = herbariumInput.action,
            inputIndex = herbariumInput.index,
            inputPosition = inputPositions[2]
        };

        testData.entries[1] = new RadialMenuEntryData()
        {
            key = "RadialMenu_Map",
            rotation = 270,
            sprite = backSprite,
            callback = CloseBackpack,
            interactable = false,
            inputAction = mapInput.action,
            inputIndex = mapInput.index,
            inputPosition = inputPositions[1]
        };
        testData.entries[3] = new RadialMenuEntryData()
        {
            key = "RadialMenu_Inventory",
            sprite = backSprite,
            rotation = 90,
            callback = () =>{ OpenInventory();},
            injectors = new object[] {
                dataHandler.GetInventorySize() - dataHandler.GetRemainingInventorySpace(),
                dataHandler.GetInventorySize() },
            interactable = true,
            inputAction = inventoryInput.action,
            inputIndex = inventoryInput.index,
            inputPosition = inputPositions[3]
        };

        if(openSound) onOpenBackpack.Invoke();

        Open(testData);
    }

    /// <summary>
	/// Opens the default inventory menu
	/// </summary>
    /// <param name="openSound">True if the opening sound should be invoked</param>
    public void OpenInventory(bool openSound = true)
    {
        RadialMenuData testData = new RadialMenuData();
        testData.radius = SIZE;
        testData.id = RadialMenuID.INVENTORY;
        testData.entries = new RadialMenuEntryData[4];
        testData.entries[0] = new RadialMenuEntryData()
        {
            key = "Close",
            sprite = backSprite,
            rotation = 0,
            callback = CloseInventory,
            interactable = true,
            inputAction = closeInput.action,
            inputIndex = closeInput.index,
            inputPosition = inputPositions[0]
        };

        PlayerDataHandler dataHandler = GameManager.instance.GetPlayerDataHandler();
        string item;

        for (int i = 0; i < 3; i++)
        {
            item = dataHandler.GetInventoryItem(i);
            testData.entries[1 + i] = new RadialMenuEntryData()
            {
                key = item == null ? "Inventory_Nothing" : item + "_Name",
                sprite = backSprite,
                rotation = -90*(i+1),
                callback = null,
                interactable = false,
                inputAction = null,
                inputIndex = 0
            };
        }

        if(openSound) onOpenInventory.Invoke();

        Open(testData);
    }

    /// <summary>
	/// Opens the inventory menu (used for giving stuff to NPC/Tables)
	/// </summary>
    /// <param name="openSound">True if the opening sound should be invoked</param>
    public void OpenInventoryGive(bool openSound = true)
    {
        RadialMenuData testData = new RadialMenuData();
        testData.radius = SIZE;
        testData.id = RadialMenuID.INVENTORY;
        testData.entries = new RadialMenuEntryData[4];
        testData.entries[0] = new RadialMenuEntryData()
        {
            key = "Close",
            sprite = backSprite,
            rotation = 0,
            callback = () => {Close(true);},
            interactable = true,
            inputAction = closeInput.action,
            inputIndex = closeInput.index,
            inputPosition = inputPositions[0]
        };

        PlayerDataHandler dataHandler = GameManager.instance.GetPlayerDataHandler();
        string item;

        for (int i = 0; i < 3; i++)
        {
            item = dataHandler.GetInventoryItem(i);
            testData.entries[1 + i] = new RadialMenuEntryData()
            {
                key = item == null ? "Inventory_Nothing" : item + "_Name",
                sprite = backSprite,
                rotation = -90*(i+1),
                callback = () => {Close(true);},
                interactable = item != null,
                inputAction = null,
                inputIndex = 0
            };
        }

        if(openSound) onOpenInventory.Invoke();

        Open(testData);
    }
}
