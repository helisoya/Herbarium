using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents the plant database
/// When asked about a plant, it will check if it is already in memory
/// If not, it will load it and store it
/// </summary>
public class PlantDatabase : MonoBehaviour
{
    private Dictionary<string, Plant> plants;
    private string[] availablePlants;

    public void Init()
    {
        plants = new Dictionary<string, Plant>();
        
        Plant[] allPlants = Resources.LoadAll<Plant>("Plants");
        availablePlants = new string[allPlants.Length];

        for(int i = 0;i < availablePlants.Length; i++)
        {
            availablePlants[i] = allPlants[i].id;
            Resources.UnloadAsset(allPlants[i]);
        }
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
    /// Gets all the plants that exists in game
    /// </summary>
    /// <returns>The plants</returns>
    public string[] GetExistingPlants()
    {
        return availablePlants;
    }

    /// <summary>
	/// Clears the plant database
    /// (Only clears the cache, not the actual plant data)
	/// </summary>
    public void ClearDatabase()
    {
        foreach(Plant value in plants.Values)
        {
            Resources.UnloadAsset(value);
        }
        plants.Clear();
    }
}
