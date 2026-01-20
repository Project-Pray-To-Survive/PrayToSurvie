using UnityEngine;

public class PlayerWalkState : AState
{
    private PlayerAnimator _playerAnimator;

    public PlayerWalkState(GameObject parent, Fsm fsm) : base(parent, fsm){}

    public override void OnStateEnter()
    {
        if (_playerAnimator == null)
        {
            _playerAnimator = _parent.GetComponent<PlayerAnimator>();
        }
        _playerAnimator.SetAnimation(_playerAnimator._walkAniState);
    }
    
    public override void OnStateExit()
    {
        
    }
}
