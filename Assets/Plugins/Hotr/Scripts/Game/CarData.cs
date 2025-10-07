using UnityEngine;

namespace MyHerbagnole
{
    /// <summary>
    /// Represents a car's data in my Herbagnole
    /// </summary>
    public class CarData : MonoBehaviour
    {
        public CarController.Wheel[] wheels;
        public Rigidbody rb;
        public Transform centerOfMass;
        public Transform driverSpot;
        public AnimatorOverrideController driverController;

    }
}

