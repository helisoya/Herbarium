using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the plant database
/// When asked about a plant, it will check if it is already in memory
/// If not, it will load it and store it
/// </summary>
public class PlantDatabase : MonoBehaviour
{
    private Dictionary<string, Plant> plants;


    public void Init()
    {
        plants = new Dictionary<string, Plant>();
    }

    /// <summary>
	/// Gets a plant from the database
    /// If it isn't in cache, it will be loaded in memory
	/// </summary>
	/// <param name="plantID">The plant's iD</param>
	/// <returns>The plant</returns>
    public Plant GetPlant(string plantID)
    {
        Plant plant;
        if (plants.TryGetValue(plantID, out plant))
        {
            return plant;
        }

        plant = Resources.Load<Plant>($"Plants/{plantID}");
        plants.Add(plant.id, plant);
        return plant;
    }

    /// <summary>
	/// Clears the plant database
    /// (Only clears the cache, not the actual plant data)
	/// </summary>
    public void ClearDatabase()
    {
        plants.Clear();
    }
}
