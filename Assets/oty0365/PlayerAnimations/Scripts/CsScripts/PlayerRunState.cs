using System.Collections;
using UnityEngine;

public class PlayerRunState : AState
{
    private PlayerAnimator _playerAnimator;
    private PlayerInputBuffer _playerInputBuffer;
    private PlayerStatus _playerStatus;
    private FsmHandler _fsmHandler;
    PlayerActionController _playerActionController;
    private Coroutine _currentCheckCanWalkCoroutine;

    public PlayerRunState(GameObject parent, Fsm fsm) : base(parent, fsm){}

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

        if (_playerStatus == null)
        {
            _playerStatus = _parent.GetComponent<PlayerStatus>();
        }

        if (_currentCheckCanWalkCoroutine != null)
        {
            CoroutineManager.Instance.StopCoroutine(_currentCheckCanWalkCoroutine);
            _currentCheckCanWalkCoroutine = null;
        }

        if (_playerActionController == null)
        {
            _playerActionController = _parent.GetComponent<PlayerActionController>();
        }

        if (_fsmHandler == null)
        {
            _fsmHandler = _parent.GetComponent<FsmHandler>();
        }
        _currentCheckCanWalkCoroutine = CoroutineManager.Instance.StartCoroutine(CheckCanWalkFlow());
        _playerAnimator.SetAnimation(_playerAnimator.WalkAniState);
        _playerAnimator.SetSpeed(2);
        if (!_playerInputBuffer.HasFlag(PlayerInputFlags.Move))
        {
            _fsmHandler.InsertToFsmQueue("Idle");
        }

    }
    
    public override void OnStateExit()
    {
        if (_currentCheckCanWalkCoroutine != null)
        {
            CoroutineManager.Instance.StopCoroutine(_currentCheckCanWalkCoroutine);
            _currentCheckCanWalkCoroutine = null;
        }
        _playerAnimator.SetSpeed(1);
    }

    private IEnumerator CheckCanWalkFlow()
    {
        while (_playerStatus.stamina.Value > _playerStatus.stamina.MinValue)
        {
            yield return null;
        }
        _fsmHandler.InsertToFsmQueue("Walk");
    }
    
}
