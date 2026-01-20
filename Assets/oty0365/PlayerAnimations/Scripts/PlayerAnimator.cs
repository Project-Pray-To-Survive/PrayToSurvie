using UnityEngine;

public class PlayerAnimator : AEntityAnimator
{
    public readonly WalkAniState _walkAniState =  new WalkAniState();
    public readonly IdleAniState _idleAniState = new IdleAniState();
}
