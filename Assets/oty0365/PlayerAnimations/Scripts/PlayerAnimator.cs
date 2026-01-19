using UnityEngine;

public class PlayerAnimator : EntityAnimator
{
    public readonly WalkAniState _walkAniState =  new WalkAniState();
    public readonly IdleAniState _idleAniState = new IdleAniState();
}
