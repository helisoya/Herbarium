using System.Collections.Generic;
using UnityEngine;

namespace MyHerbagnole
{
    /// <summary>
    /// Represents a trigger for a lap in MyHerbagnole
    /// </summary>
    public class LapTrigger : MonoBehaviour
    {
        private List<CarController> cheaters = new List<CarController>();


        // Positive : Player comes from forward (not intended)
        // Negative : Player comes from backward (intended)

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                CarController controller = other.attachedRigidbody.transform.parent.parent.GetComponent<CarController>();
                float dotProduct = Vector3.Dot((other.attachedRigidbody.position - transform.position), transform.forward);

                if (!cheaters.Contains(controller)) // If already flagged as cheater, don't do anything
                {
                    if (dotProduct > 0) // Cheating
                    {
                        cheaters.Add(controller);
                    }
                    else // All good
                    {
                        other.attachedRigidbody.transform.parent.parent.GetComponent<CarController>().FlagNextLap();
                    }
                }
                
            }
        }
        
        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                CarController controller = other.attachedRigidbody.transform.parent.parent.GetComponent<CarController>();
                float dotProduct = Vector3.Dot((other.attachedRigidbody.position - transform.position), transform.forward);

                if (cheaters.Contains(controller) && dotProduct > 0) // Cheated and is exiting by the intended way
                {
                    cheaters.Remove(controller);
                }
            }
        }
        
    }
}

