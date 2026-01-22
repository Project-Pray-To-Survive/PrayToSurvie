using System.Collections;
using UnityEngine;

public class PlayerWalkState : AState
{
    private PlayerAnimator _playerAnimator;
    private PlayerInputBuffer _playerInputBuffer;
    private PlayerActionController _playerActionController;
    private FsmHandler _fsmHandler;
    private Coroutine _currentCheckCanRunCoroutine;

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

        if (_fsmHandler == null)
        {
            _fsmHandler = _parent.GetComponent<FsmHandler>();
        }
        _playerAnimator.SetAnimation(_playerAnimator.WalkAniState);
        if (_currentCheckCanRunCoroutine != null)
        {
            CoroutineManager.Instance.StopCoroutine(_currentCheckCanRunCoroutine);
        }
        _currentCheckCanRunCoroutine = CoroutineManager.Instance.StartCoroutine(CheckCanRun());
    }
    
    public override void OnStateExit()
    {
        if (_currentCheckCanRunCoroutine == null) return;
        CoroutineManager.Instance.StopCoroutine(_currentCheckCanRunCoroutine);
        _currentCheckCanRunCoroutine = null;
    }

    private IEnumerator CheckCanRun()
    {
        while (true)
        {
            if (_playerInputBuffer.HasFlag(PlayerInputFlags.Sprint)&&!_playerActionController.PlayerSprintLogic.IsTiered)
            {
                _fsmHandler.InsertToFsmQueue("Run");
                yield break;
            }
            yield return null;
        }

    }
}
