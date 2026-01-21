using UnityEngine;

public class PlayerIdleState : AState
{
    private PlayerAnimator _playerAnimator;
    private PlayerInputBuffer _playerInputBuffer;

    public PlayerIdleState(GameObject parent, Fsm fsm) : base(parent, fsm){}

    public override void OnStateEnter()
    {
        if (_playerAnimator == null)
        {
            _playerAnimator = _parent.GetComponent<PlayerAnimator>();
        }

        if (_playerInputBuffer == null)
        {
            _playerInputBuffer = _parent.GetComponent<PlayerInputBuffer>();
        }

        _playerAnimator.SetAnimation(_playerAnimator.IdleAniState);
        if (_playerInputBuffer.HasFlag(PlayerInputFlags.Move))
        {
            _fsm.ChangeState("Walk");
        }
    }
    
    public override void OnStateExit()
    {
        
    }
}
