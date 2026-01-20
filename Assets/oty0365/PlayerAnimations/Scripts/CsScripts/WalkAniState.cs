using UnityEngine;

public class WalkAniState : IAniState
{
    public void SetState(Animator animator)
    {
        animator.SetInteger("Behave",1);
    }
}
