using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

/// <summary>
/// Handles the players data
/// Is used to save & load the data, as well as modifying it
/// </summary>
public class PlayerDataHandler : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private int inventorySize;
    [SerializeField] private int dialogLogMaxSize;
    [SerializeField] private DefaultPlayerVariables variables;
    [SerializeField] private PlayerQuests quests;
    [SerializeField] private SerializedDictionary<string,int> defaultPlantRegrowth;

    public string filePath
    {
        get
        {
            return FileManager.savPath + "save.sav";
        }
    }

    public bool fileExistsOnDisk
    {
        get
        {
            return System.IO.File.Exists(filePath);
        }
    }

    private PlayerData data;


    #region Quests

    /// <summary>
    /// Gets a quest
    /// </summary>
    /// <param name="quest">The quest ID</param>
    /// <returns>The quest</returns>
    public Quest GetQuest(string quest)
    {
        foreach(Quest q in quests.quests)
        {
            if(q.id.Equals(quest)) return q;
        }

        return new Quest();
    }

    /// <summary>
    /// Gets a quest using its linked variable
    /// </summary>
    /// <param name="variable">The variable</param>
    /// <returns>The quest</returns>
    public Quest GetQuestFromVariable(string variable)
    {
        foreach(Quest q in quests.quests)
        {
            if(q.linkedVariable.Equals(variable)) return q;
        }

        return new Quest();
    }

    /// <summary>
    /// Gets all existing quests
    /// </summary>
    /// <returns>Every quests</returns>
    public Quest[] GetAllQuests()
    {
        return quests.quests;
    }

    /// <summary>
    /// Gets known quests
    /// </summary>
    /// <returns>The known quests</returns>
    public Quest[] GetKnownQuests()
    {
        List<Quest> result = new List<Quest>();

        foreach(Quest quest in quests.quests)
        {
            if(GetVariable(quest.linkedVariable) > -1) result.Add(quest);
        }

        return result.ToArray();
    }

    /// <summary>
    /// Gets the index of a known quest given its ID
    /// </summary>
    /// <param name="questID">The quest's ID</param>
    /// <returns>Its index in the known quests array</returns>
    public int KnownQuestIDToIndex(string questID)
    {
        Quest[] quests = GetKnownQuests();

        for(int i = 0; i < quests.Length; i++)
        {
            if(quests[i].id.Equals(questID)) return i;
        }

        return 0;
    }

    /// <summary>
    /// Pin a quest
    /// </summary>
    /// <param name="questID">The quest's ID</param>
    public void PinQuest(string questID)
    {
        if(!data.pinnedQuests.Contains(questID)) data.pinnedQuests.Add(questID);
    }

    /// <summary>
    /// Unpin a quest
    /// </summary>
    /// <param name="questID">The quest's ID</param>
    public void UnpinQuest(string questID)
    {
        data.pinnedQuests.Remove(questID);
    }

    /// <summary>
    /// Switch if a quest is pinned or not
    /// </summary>
    /// <param name="questID">The quest ID</param>
    public void SwitchQuestPin(string questID)
    {
        if(!data.pinnedQuests.Remove(questID)) data.pinnedQuests.Add(questID);
    }

    /// <summary>
    /// Checks if a quest is pinned
    /// </summary>
    /// <param name="questID">The quest's ID</param>
    /// <returns>True if pinned</returns>
    public bool IsPinned(string questID)
    {
        return data.pinnedQuests.Contains(questID);
    }

    #endregion

    #region Herbarium

    /// <summary>
	/// Gets the unlocked Herbarium pages
	/// </summary>
	/// <returns>The pages</returns>
    public string[] GetHerbariumUnlockedPages()
    {
        return data.herbarium.ToArray();
    }

    /// <summary>
	/// Adds a plant page to the Herbarium
	/// </summary>
	/// <param name="plantID">The plant ID</param>
    public void AddHerbariumPage(string plantID)
    {
        if (!data.herbarium.Contains(plantID)) data.herbarium.Add(plantID);
    }

    /// <summary>
    /// Checks if a plant is unlocked in the herbarium
    /// </summary>
    /// <param name="plantID">The plant ID</param>
    /// <returns>True if the plant is unlocked</returns>
    public bool IsUnlockedInHerbarium(string plantID)
    {
        return data.herbarium.Contains(plantID);
    }

    #endregion

    #region Log

    /// <summary>
	/// Adds a new dialog to the log
	/// </summary>
	/// <param name="dialogId">The dialog's ID</param>
    /// <param name="speakerId">The speaker's ID</param>
    public void AddDialogLog(string dialogId, string speakerId)
    {
        if (data.dialogLog.Count == dialogLogMaxSize) data.dialogLog.RemoveAt(0);
        data.dialogLog.Add(new DialogLog(){dialogId = dialogId, speakerId = speakerId});
    }

    /// <summary>
	/// Clears the dialog logs
	/// </summary>
    public void ClearLog()
    {
        data.dialogLog.Clear();
    }

    /// <summary>
	/// Returns the dialog logs
	/// </summary>
	/// <returns>The dialog logs</returns>
    public DialogLog[] GetLog()
    {
        return data.dialogLog.ToArray(); ;
    }

    #endregion

    #region Inventory

    /// <summary>
    /// Gets the remaining space in the inventory
    /// </summary>
    /// <returns>The remaining space in the inventory</returns>
    public int GetRemainingInventorySpace()
    {
        int remainingSpace = inventorySize;
        for (int i = 0; i < inventorySize; i++)
        {
            if (data.inventory[i] != null) remainingSpace--;
        }
        return remainingSpace;
    }

    /// <summary>
	/// Removes an item from the inventory
	/// </summary>
	/// <param name="index">The item's index</param>
	/// <returns>The removed item</returns>
    public string RemoveFromInventoryAt(int index)
    {
        if (index < 0 || index >= data.inventory.Length) return null;

        string result = data.inventory[index];
        data.inventory[index] = null;
        return result;
    }

    /// <summary>
	/// Adds an item to the inventory
	/// </summary>
	/// <param name="itemId">The item ID</param>
    /// <returns>True if the operation was a success</returns>
    public bool AddInInventory(string itemId)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (data.inventory[i] == null)
            {
                data.inventory[i] = itemId;
                return true;
            }
        }
        return false;
    }

    /// <summary>
	/// Gets an item from the inventory
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
    public string GetInventoryItem(int index)
    {
        if (index < 0 || index >= data.inventory.Length) return null;
        return data.inventory[index];
    }

    /// <summary>
	/// Gets the inventory size
	/// </summary>
	/// <returns>The inventory size</returns>
    public int GetInventorySize()
    {
        return inventorySize;
    }

    #endregion

    #region Tutorials

    /// <summary>
    /// Sets if the foraging tutorial was completed or not
    /// </summary>
    /// <param name="value">True if it was</param>
    public void SetHasCompletedForagingTutorial(bool value)
    {
        data.hadForagingTutorial = value;
    }

    /// <summary>
    /// Gets if the foraging tutorial was completed or not 
    /// </summary>
    /// <returns>True if it was</returns>
    public bool HasCompletedForagingTutorial()
    {
        return data.hadForagingTutorial;
    }

    #endregion

    #region Regrowth

    /// <summary>
    /// Checks if a plant entity has regrown or not
    /// </summary>
    /// <param name="entityId">The entity's Id</param>
    /// <returns>True if the plant has regrown</returns>
    public bool HasEntityRegrown(string entityId)
    {
        foreach(RegrowthPlantData data in data.regrowthData)
        {
            if(data.entityId.Equals(entityId)) return false;
        }
        return true;
    }

    /// <summary>
    /// Increments the regrowth system
    /// </summary>
    public void IncrementRegrowthSystem()
    {
        int idx = 0;
        while(idx < data.regrowthData.Count)
        {
            data.regrowthData[idx].regrowthTime -= 1;
            if(data.regrowthData[idx].regrowthTime <= 0)
            {
                data.regrowthData.RemoveAt(idx);
            }
            else
            {
                idx++;
            }
        }
    }

    /// <summary>
    /// Register a new entity to the regrowth system
    /// </summary>
    /// <param name="plantId">The entity's linked plant</param>
    /// <param name="entityId">The entity's Id</param>
    public void RegisterEntityHasNotRegrown(string plantId, string entityId)
    {
        if(!HasEntityRegrown(entityId)) return;

        data.regrowthData.Add(new RegrowthPlantData(){entityId=entityId,regrowthTime=GetDefaultPlantRegrowthSpeed(plantId)});
    }

    /// <summary>
    /// Gets the default regrowth time for a plant
    /// </summary>
    /// <param name="plantId">The plant Id</param>
    /// <returns>The plant's regrowth speed</returns>
    public int GetDefaultPlantRegrowthSpeed(string plantId)
    {
        if(defaultPlantRegrowth.TryGetValue(plantId, out int value)) return value;
        return 0;
    }

    #endregion

    #region Map & Positions

    /// <summary>
    /// Gets the current map
    /// </summary>
    /// <returns>The current map</returns>
    public string GetCurrentMap()
    {
        return data.mapName;
    }

    /// <summary>
    /// Sets the current map
    /// </summary>
    /// <param name="mapName">The current map name</param>
    public void SetCurrentMap(string mapName)
    {
        data.mapName = mapName;
    }

    /// <summary>
    /// Gets the current map position
    /// </summary>
    /// <returns>The current map position</returns>
    public Vector3 GetMapPosition()
    {
        return data.mapPosition;
    }

    /// <summary>
    /// Sets the current map position
    /// </summary>
    /// <param name="position">The new position</param>
    public void SetMapPosition(Vector3 position)
    {
        data.mapPosition = position;
    }

    #endregion

    #region Variables

    /// <summary>
	/// Sets a variable's value
	/// </summary>
	/// <param name="id">The variable's id</param>
	/// <param name="value">The new value</param>
    public void SetVariable(string id, int value)
    {
        for (int i = 0; i < data.variables.Length; i++)
        {
            if (data.variables[i].id.Equals(id))
            {
                data.variables[i].value = value;
                return;
            }
        }
    }

    /// <summary>
	/// Gets a variable's value
	/// </summary>
	/// <param name="id">The variable's id</param>
	/// <returns>The variable's value</returns>
    public int GetVariable(string id)
    {
        for (int i = 0; i < data.variables.Length; i++)
        {
            if (data.variables[i].id.Equals(id))
            {
                return data.variables[i].value;
            }
        }
        return 0;
    }

    #endregion

    #region Save, Load & Control

    /// <summary>
    /// Resets the player data
    /// </summary>
    public void ResetData()
    {
        data = new PlayerData();
        data.inventory = new string[inventorySize];
        data.dialogLog = new List<DialogLog>();
        data.herbarium = new List<string>();
        data.regrowthData = new List<RegrowthPlantData>();
        data.pinnedQuests = new List<string>();

        data.variables = new PlayerVariable[variables.variables.Length];
        for (int i = 0; i < variables.variables.Length; i++)
        {
            data.variables[i] = new PlayerVariable()
            {
                id = variables.variables[i].id,
                value = variables.variables[i].value
            };
        }
    }

    /// <summary>
	/// Loads the player data from disk
	/// </summary>
    public void LoadData()
    {
        PlayerVariable[] finalData = new PlayerVariable[variables.variables.Length];
        for (int i = 0; i < variables.variables.Length; i++)
        {
            finalData[i] = new PlayerVariable()
            {
                id = variables.variables[i].id,
                value = variables.variables[i].value
            };
        }

        PlayerData data = FileManager.LoadJSON<PlayerData>(filePath);

        for (int i = 0; i < data.variables.Length; i++)
        {
            for (int j = 0; j < finalData.Length; i++)
            {
                if (data.variables[i].id.Equals(finalData[j].id))
                {
                    finalData[j].value = data.variables[i].value;
                    break;
                }
            }
        }

        data.variables = finalData;
        for(int i = 0; i < data.inventory.Length; i++)
        {
            if (string.IsNullOrEmpty(data.inventory[i]))
            {
                data.inventory[i] = null;
            }
        }


        this.data = data;
    }

    /// <summary>
	/// Saves the player data to disk
	/// </summary>
    public void SaveData()
    {
        if (!GameManager.instance.inMainMenu)
        {
            data.mapPosition = Player.instance.position;
        }
        FileManager.SaveJSON(filePath, data);
    }

    #endregion
}