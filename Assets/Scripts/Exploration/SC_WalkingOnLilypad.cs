using NUnit.Framework.Internal;
using UnityEngine;

public class SC_WalkingOnLilypad : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        _animator.SetTrigger("Down");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        _animator.SetTrigger("Squish");
    }
}
