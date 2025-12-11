using UnityEngine;

/// <summary>
/// Handles the players data
/// Is used to save & load the data, as well as modifying it
/// </summary>
public class PlayerDataHandler : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private int inventorySize;

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



    #region Save, Load & Control

    /// <summary>
	/// Resets the player data
	/// </summary>
    public void ResetData()
    {
        data = new PlayerData();
        data.inventory = new string[inventorySize];
    }

    /// <summary>
	/// Loads the player data from disk
	/// </summary>
    public void LoadData()
    {
        FileManager.LoadJSON<PlayerData>(filePath, ref data);
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
