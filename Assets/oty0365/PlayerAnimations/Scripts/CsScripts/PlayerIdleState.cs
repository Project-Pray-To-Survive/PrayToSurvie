using UnityEngine;

public class PlayerIdleState : AState
{
    private PlayerAnimator _playerAnimator;

    public PlayerIdleState(GameObject parent, Fsm fsm) : base(parent, fsm){}

    public override void OnStateEnter()
    {
        if (_playerAnimator == null)
        {
            _playerAnimator = _parent.GetComponent<PlayerAnimator>();
        }
        _playerAnimator.SetAnimation(_playerAnimator._idleAniState);
    }
    
    public override void OnStateExit()
    {
        
    }
}
