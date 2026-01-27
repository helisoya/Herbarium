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
    private string[] availableSecretPlants;

    public void Init()
    {
        plants = new Dictionary<string, Plant>();
        
        List<string> normalPlants = new List<string>();
        List<string> secretPlants = new List<string>();

        Plant[] allPlants = Resources.LoadAll<Plant>("Plants");

        for(int i = 0;i < allPlants.Length; i++)
        {
            if (allPlants[i].isSecret)
            {
                secretPlants.Add(allPlants[i].id);
            }
            else
            {
                normalPlants.Add(allPlants[i].id);
            }
            Resources.UnloadAsset(allPlants[i]);
        }

        availablePlants = normalPlants.ToArray();
        availableSecretPlants = secretPlants.ToArray();
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
    /// Converts a plant ID to a plant index
    /// </summary>
    /// <param name="plantId">The plant ID</param>
    /// <returns>The plant's index</returns>
    public int PlantIDToIndex(string plantId)
    {
        int i = 0;
        while(i < availablePlants.Length)
        {
            if(availablePlants[i].Equals(plantId)) return i;
            i++;
        }
        while(i < availableSecretPlants.Length)
        {
            if(availableSecretPlants[i].Equals(plantId)) return i;
            i++;
        }

        return 0;
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
    /// Gets all the secret plants that exists in game
    /// </summary>
    /// <returns>The secret plants</returns>
    public string[] GetExistingSecretPlants()
    {
        return availableSecretPlants;
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
