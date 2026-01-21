using UnityEngine;

public class PlayerWalkState : AState
{
    private PlayerAnimator _playerAnimator;
    private PlayerInputBuffer _playerInputBuffer;
    private PlayerActionController _playerActionController;

    public PlayerWalkState(GameObject parent, Fsm fsm) : base(parent, fsm){}

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

        if (_playerActionController == null)
        {
            _playerActionController = _parent.GetComponent<PlayerActionController>();
        }
        
        _playerAnimator.SetAnimation(_playerAnimator.WalkAniState);
        if (_playerInputBuffer.HasFlag(PlayerInputFlags.Sprint))
        {
            _fsm.ChangeState("Run");
        }
    }
    
    public override void OnStateExit()
    {
        
    }
}
