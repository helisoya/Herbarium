using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField] private Animator shearsAnimator;

    [Header("Plant")]
    [SerializeField] private Transform plantRoot;
    private Joint2D[] plantJoints;
    private int plantHP;
    private int plantMaxHP;

    [Header("GUI")]
    [SerializeField] private RectTransform plantHealthRoot;
    [SerializeField] private RectTransform plantHealthPrefab;
    private List<RectTransform> plantHealthParts;
    [SerializeField] private GameObject tutorialRoot;

    [Header("Backpack")]
    [SerializeField] private GameObject backpackDone;
    [SerializeField] private GameObject backpackNotDone;
    private bool plantInBackpack;

    [Header("Foraging Audio")]
    [SerializeField] private UnityEvent onCut;
    [SerializeField] private UnityEvent onCutGood;
    [SerializeField] private UnityEvent onCutBad;
    [SerializeField] private UnityEvent onPlantInBag;
    [SerializeField] private UnityEvent<float> onPlantUpdateLife;


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
        backpackNotDone.SetActive(true);
        backpackDone.SetActive(false);
        plantInBackpack = false;

        if (debugAutoPlay && debugPlayer)
        {
            debugPlayer.StartMicroInteraction(this, debugPlantId);
        }
    }

    protected override void OnStart(string plantID)
    {
        Plant plant = GameManager.instance.GetPlantDatabase().GetPlant(plantID);
        plantHP = plant.foragingHealth;
        plantMaxHP = plantHP;

        Transform prefab = Instantiate(plant.foragingPrefab, plantRoot);
        plantJoints = prefab.GetChild(0).GetComponentsInChildren<Joint2D>();

        tutorialRoot.SetActive(!GameManager.instance.GetPlayerDataHandler().HasCompletedForagingTutorial());

        cameraStartPos = microInteractionCamera.transform.localPosition;
        onPlantUpdateLife.Invoke(1.0f);

        float radiansSeparation = Mathf.PI * 2 / plant.foragingHealth;
        RectTransform rectTransform;
        plantHealthParts = new List<RectTransform>();

        for (int i = 0; i < plant.foragingHealth; i++)
        {
            rectTransform = Instantiate(plantHealthPrefab, plantHealthRoot);

            rectTransform.anchoredPosition = new Vector2(
                Mathf.Sin(Mathf.PI + radiansSeparation * i) * 17.0f,
                Mathf.Cos(Mathf.PI + radiansSeparation * i) * 17.0f
            );

            rectTransform.localEulerAngles = new Vector3(0, 0, (radiansSeparation * (-i) + Mathf.PI) * Mathf.Rad2Deg);

            plantHealthParts.Add(rectTransform);
        }
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
        if (currentShake > 0)
        {
            microInteractionCamera.transform.localPosition = cameraStartPos + Random.insideUnitSphere * Settings.instance.GetScreenshakeStrength();
            currentShake -= Time.deltaTime * shakeDecay;

        }
        else
        {
            microInteractionCamera.transform.localPosition = cameraStartPos;
        }
    }

    protected override void OnToolUse()
    {

        if (currentObject.GetPickableType() == MicroInteractionPickable.PickableType.CUTTER)
        {
            shearsAnimator.SetTrigger("Cutting");
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
                plantHealthParts[plantHealthParts.Count - 1].DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuad);
                plantHealthParts.RemoveAt(plantHealthParts.Count - 1);
                currentShake = shakeAmount;
                onPlantUpdateLife.Invoke(plantHP / (float)plantMaxHP);
                if (plantHP <= 0)
                {
                    foreach (Joint2D joint in plantJoints)
                    {
                        if (joint) joint.enabled = false;
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
        onPlantInBag.Invoke();
        backpackNotDone.SetActive(false);
        backpackDone.SetActive(true);
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
