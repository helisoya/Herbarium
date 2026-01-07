using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Handles the foraging micro interaction
/// </summary>
public class ForagingMicroInteraction : MicroInteraction
{

    [SerializeField] private float cutForwardLength = 1f;
    [SerializeField] private LayerMask mask;

    [Header("Plant")]
    [SerializeField] private Transform plantRoot;
    private Joint2D[] plantJoints;
    private int plantHP;

    [Header("GUI")]
    [SerializeField] private TextMeshProUGUI plantHealthText;

    [Header("Backpack")]
    [SerializeField] private GameObject backpackTopCollider;
    private bool plantInBackpack;

    [Header("Debug")]
    [SerializeField] private bool debugAutoPlay;
    [SerializeField] private string debugPlantId = "TestPlant1";
    [SerializeField] private Player debugPlayer;

    void Start()
    {
        backpackTopCollider.SetActive(false);
        plantInBackpack = false;

        if (debugAutoPlay)
        {
            debugPlayer.StartMicroInteraction(this,debugPlantId);
        } 
    }

    protected override void OnStart(string plantID)
    {
        Plant plant = GameManager.instance.GetPlantDatabase().GetPlant(plantID);
        plantHP = plant.foragingHealth;
        plantHealthText.text = plantHP.ToString();
        
        Transform prefab = Instantiate(plant.foragingPrefab,plantRoot);
        plantJoints = prefab.GetChild(0).GetComponentsInChildren<Joint2D>();
    }

    protected override void OnEnd(EndingType type)
    {
    }


    void OnDrawGizmosSelected()
    {
        if (currentObject)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(currentObject.transform.position, currentObject.transform.position + currentObject.transform.up * cutForwardLength);
        }
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnToolUse()
    {
            if(currentObject.GetPickableType() == ForagingPickable.PickableType.CUTTER)
            {
                bool cutFound = false;
                RaycastHit2D[] hits = Physics2D.RaycastAll(currentObject.transform.position, currentObject.transform.up, cutForwardLength, mask);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.rigidbody.gameObject.TryGetComponent<ForagingCutPoint>(out ForagingCutPoint cutPoint))
                    {
                        cutPoint.Cut();
                        cutFound = true;
                        break;
                    }
                }

                if (!cutFound && hits.Length > 0)
                {
                    // Unless you can interact with a non plant rigidbody (the dropped flower for instance), everything will be fine ?
                    plantHP--;
                    plantHealthText.text = plantHP.ToString();
                    if(plantHP <= 0)
                    {
                        foreach (Joint2D joint in plantJoints) {
                            if(joint) joint.enabled = false;
                        }
                        EndInteraction(EndingType.FAILURE);
                    }
                }
            }
    }

    /// <summary>
    /// Raise the plant in backpack flag
    /// </summary>
    public void RaiseFlagPlantInBackpack()
    {
        backpackTopCollider.SetActive(true);
        plantInBackpack = true;
        CloseMicroInteraction();
    }

    /// <summary>
    /// Closes the micro interaction
    /// </summary>
    public void CloseMicroInteraction()
    {
        EndInteraction(plantInBackpack ? EndingType.SUCCESS : EndingType.CANCEL);
    }
}
