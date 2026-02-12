using UnityEngine;

/// <summary>
/// Handles Barbrook Animation events
/// </summary>
public class BarbrookAnimations : MonoBehaviour
{
    [SerializeField] private Animator dreamAnimator;

    public void OnBarbrookSleep()
    {
        dreamAnimator.SetTrigger("Sleep");
    }

    public void OnBarbrookWakeUp()
    {
        dreamAnimator.SetTrigger("WakeUp");
    }
}
