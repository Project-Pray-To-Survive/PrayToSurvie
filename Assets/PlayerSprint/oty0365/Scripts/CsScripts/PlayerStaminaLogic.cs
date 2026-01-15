using System.Collections;
using UnityEngine;

public class PlayerStaminaLogic
{
    private readonly PlayerStatus _playerStatus;
    private Coroutine _currentFillCoroutine;

    public PlayerStaminaLogic(PlayerStatus playerStatus)
    {
        _playerStatus = playerStatus;
    }

    public void StartReFill()
    {
        if (_currentFillCoroutine != null)
        {
            CoroutineManager.Instance.StopRoutine(_currentFillCoroutine);
            _currentFillCoroutine = null;
        }
        _currentFillCoroutine = CoroutineManager.Instance.StartRoutine(FillFlow());
    }
    
    public void StopReFill()
    {
        if (_currentFillCoroutine != null)
        {
            CoroutineManager.Instance.StopRoutine(_currentFillCoroutine);
            _currentFillCoroutine = null;
        }
    }
    
    private IEnumerator FillFlow()
    {
        while (_playerStatus.stamina.Value <= _playerStatus.stamina.MaxValue)
        {
            _playerStatus.stamina.Value += _playerStatus.BaseStaminaFillSpeed*Time.deltaTime;
            yield return null;
        }
        _currentFillCoroutine = null;
    }
}
