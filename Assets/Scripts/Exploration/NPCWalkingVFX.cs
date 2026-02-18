using UnityEngine;
using UnityEngine.VFX;

public class NPCWalkingVFX : MonoBehaviour
{
    public Animator animator;
    public VisualEffect vfx;

    void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        bool isWalking = state.IsName("AN_Apprentice_Walk_Right") || state.IsName("AN_Apprentice_Walk");
        vfx.SetBool("isWalking", isWalking);
    }
}