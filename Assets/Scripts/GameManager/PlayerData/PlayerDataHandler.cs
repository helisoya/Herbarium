using System.Collections.Generic;
using System.Linq;
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
	/// <param name="logID">The dialog local's ID</param>
    public void AddDialogLog(string logID)
    {
        if (data.dialogLog.Count == dialogLogMaxSize) data.dialogLog.RemoveFirst();
        data.dialogLog.AddLast(logID);
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
    public string[] GetLog()
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
        data.dialogLog = new System.Collections.Generic.LinkedList<string>();
        data.herbarium = new System.Collections.Generic.List<string>();
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

        this.data = data;
    }

    /// <summary>
	/// Saves the player data to disk
	/// </summary>
    public void SaveData()
    {
        FileManager.SaveJSON(filePath, data);
    }

    #endregion
}
