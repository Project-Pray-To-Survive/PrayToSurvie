using UnityEngine;

public class IdleAniState : IAniState
{
    public void SetState(Animator animator)
    {
        animator.SetInteger("Behave",0);
    }
}
