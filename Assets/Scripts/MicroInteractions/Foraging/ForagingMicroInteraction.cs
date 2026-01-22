using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the foraging micro interaction
/// </summary>
public class ForagingMicroInteraction : MicroInteraction
{
    [Header("Foraging")]
    [SerializeField] private float cutForwardLength = 1f;
    [SerializeField] private LayerMask mask;

    [Header("Plant")]
    [SerializeField] private Transform plantRoot;
    private Joint2D[] plantJoints;
    private int plantHP;
    private int plantMaxHP;

    [Header("GUI")]
    [SerializeField] private TextMeshProUGUI plantHealthText;
    [SerializeField] private GameObject tutorialRoot;

    [Header("Backpack")]
    [SerializeField] private GameObject backpackTopCollider;
    private bool plantInBackpack;

    [Header("Foraging Audio")]
    [SerializeField] private UnityEvent onCut;
    [SerializeField] private UnityEvent onCutGood;
    [SerializeField] private UnityEvent onCutBad;
    [SerializeField] private UnityEvent onPlantInBag;
    [SerializeField] private UnityEvent<float> onPlantLifeUpdate;
    [SerializeField] private UnityEvent<string> onChangePlantID;


    [Header("ScreenShake")]
    [SerializeField] private float shakeAmount = 1.0f;
    [SerializeField] private float shakeDecay = 0.7f;
    private float currentShake;
    private Vector3 cameraStartPos;


    [Header("Debug")]
    [SerializeField] private bool debugAutoPlay;
    [SerializeField] private string debugPlantId = "TestPlant1";
    [SerializeField] private Player debugPlayer;

    void Start()
    {
        backpackTopCollider.SetActive(false);
        plantInBackpack = false;

        if (debugAutoPlay && debugPlayer)
        {
            debugPlayer.StartMicroInteraction(this,debugPlantId);
        } 
    }

    protected override void OnStart(string plantID)
    {
        onChangePlantID.Invoke(plantID);

        Plant plant = GameManager.instance.GetPlantDatabase().GetPlant(plantID);
        plantHP = plant.foragingHealth;
        plantMaxHP = plantHP;
        plantHealthText.text = plantHP.ToString();
        
        Transform prefab = Instantiate(plant.foragingPrefab,plantRoot);
        plantJoints = prefab.GetChild(0).GetComponentsInChildren<Joint2D>();

        tutorialRoot.SetActive(!GameManager.instance.GetPlayerDataHandler().HasCompletedForagingTutorial());

        cameraStartPos = microInteractionCamera.transform.localPosition;
        onPlantLifeUpdate.Invoke(1.0f);
    }

    protected override void OnEnd(EndingType type)
    {
        
    }


    void OnDrawGizmos()
    {
        if (currentObject)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(currentObject.GetCurrentMovingPart().transform.position, currentObject.GetCurrentMovingPart().transform.position + currentObject.GetCurrentMovingPart().transform.up * cutForwardLength);
        }
    }

    protected override void OnUpdate()
    {
        if (currentShake > 0) {
            microInteractionCamera.transform.localPosition = cameraStartPos + Random.insideUnitSphere * Settings.instance.GetScreenshakeStrength();            
            currentShake -= Time.deltaTime * shakeDecay;

        } else {
            microInteractionCamera.transform.localPosition = cameraStartPos;
        }   
    }

    protected override void OnToolUse()
    {
        if(currentObject.GetPickableType() == MicroInteractionPickable.PickableType.CUTTER)
        {
            onCut.Invoke();
            bool cutFound = false;
            MicroInteractionPickablePart part = currentObject.GetCurrentMovingPart();
            RaycastHit2D[] hits = Physics2D.RaycastAll(part.transform.position, part.transform.up, cutForwardLength, mask);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.rigidbody.gameObject.TryGetComponent<ForagingCutPoint>(out ForagingCutPoint cutPoint))
                {
                    onCutGood.Invoke();
                    cutPoint.Cut();
                    cutFound = true;
                    break;
                }
            }

            if (!cutFound && hits.Length > 0)
            {
                // Unless you can interact with a non plant rigidbody (the dropped flower for instance), everything will be fine ?
                onCutBad.Invoke();
                plantHP--;
                plantHealthText.text = plantHP.ToString();
                currentShake = shakeAmount;
                onPlantLifeUpdate.Invoke((float)plantHP/plantMaxHP);
                if(plantHP <= 0)
                {
                    if (currentObject)
                    {
                        currentObject.Drop();
                        currentObject = null;
                    }
                    foreach (Joint2D joint in plantJoints) {
                        if(joint) joint.enabled = false;
                    }
                    EndInteraction(EndingType.FAILURE);
                }
            }
        }
    }

    /// <summary>
    /// Opens the tutorial window
    /// </summary>
    public void OpenTutorialPopup()
    {
        tutorialRoot.SetActive(true);
    }

    /// <summary>
    /// Closes the tutorial window
    /// </summary>
    public void CloseTutorialPopup()
    {
        tutorialRoot.SetActive(false);
        GameManager.instance.GetPlayerDataHandler().SetHasCompletedForagingTutorial(true);
    }

    /// <summary>
    /// Raise the plant in backpack flag
    /// </summary>
    public void RaiseFlagPlantInBackpack()
    {
        if (currentObject)
        {
            currentObject.Drop();
            currentObject = null;
        }

        onPlantInBag.Invoke();
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
