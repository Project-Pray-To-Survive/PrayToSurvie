using System;
using System.Collections;
using UnityEngine;

public class PlayerSprintLogic
{
    public event Action OnSprintStart;
    public event Action OnMoveSpeedUpdate;
    public event Action OnSprintEnd;
    private readonly PlayerStatus _playerStatus;
    private Coroutine _currentSprintCoroutine;

    public PlayerSprintLogic(PlayerStatus playerStatus)
    {
        _playerStatus = playerStatus;
    }

    public void OnSprint(bool input)
    {
        if (input)
        {
            if(_playerStatus.stamina.Value <= _playerStatus.staminaRegenRate.Value/100*_playerStatus.stamina.MaxValue) return;
            if (_currentSprintCoroutine != null) return;
            _currentSprintCoroutine = CoroutineManager.Instance.StartRoutine(SprintFlow());
        }
        else
        {
            if (_currentSprintCoroutine != null)
            {
                CoroutineManager.Instance.StopRoutine(_currentSprintCoroutine);
                _currentSprintCoroutine = null;
            }
            _playerStatus.moveSpeed.Value = _playerStatus.BaseMoveSpeed;
            OnMoveSpeedUpdate?.Invoke();
            OnSprintEnd?.Invoke();
        }
    }

    private IEnumerator SprintFlow()
    {
        if (_playerStatus.stamina.Value <= 0)
        {
            _currentSprintCoroutine = null;
            yield break;
        }
        _playerStatus.moveSpeed.Value = _playerStatus.BaseSprintSpeed;
        OnSprintStart?.Invoke();
        OnMoveSpeedUpdate?.Invoke();
        while (_playerStatus.stamina.Value > 0)
        {
            _playerStatus.stamina.Value-=_playerStatus.BaseStaminaDecreaseSpeed*Time.deltaTime;
            yield return null;
        }
        _playerStatus.moveSpeed.Value = _playerStatus.BaseMoveSpeed;
        OnSprintEnd?.Invoke();
        OnMoveSpeedUpdate?.Invoke();
        _currentSprintCoroutine = null;
    }
    
}
