using UnityEngine;

public class PlayerAnimator : AEntityAnimator
{
    public readonly WalkAniState WalkAniState =  new WalkAniState();
    public readonly IdleAniState IdleAniState = new IdleAniState();
}
