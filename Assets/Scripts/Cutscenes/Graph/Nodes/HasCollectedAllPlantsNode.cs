using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that checks if the player has collected all plants
/// </summary>
[CreateNodeMenu("Control/Has Collected All Plants")]
public class HasCollectedAllPlantsNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string[] plantsToCheck;
    [Output(connectionType = ConnectionType.Override)] public bool exitAllCollected;
    [Output(connectionType = ConnectionType.Override)] public bool exitNotAllCollected;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        string[] final = plantsToCheck;
        if(final == null || final.Length == 0)
        {
            final = GameManager.instance.GetPlantDatabase().GetExistingPlants();
        }

        bool good = true;

        foreach(string plant in final)
        {
            if (!GameManager.instance.GetPlayerDataHandler().IsUnlockedInHerbarium(plant))
            {
                good = false;
                break;
            }
        }

        if (good)
        {
            yield return 0;
        }
        else
        {
            yield return 1;
        }
    }
}
