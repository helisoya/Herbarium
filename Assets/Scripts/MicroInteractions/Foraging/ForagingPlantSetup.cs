using UnityEngine;

/// <summary>
/// Helps setup various joints and their variables
/// </summary>
public class ForagingPlantSetup : MonoBehaviour
{
    [SerializeField] private float value;


    void Start()
    {
        Destroy(this);
    }
}
