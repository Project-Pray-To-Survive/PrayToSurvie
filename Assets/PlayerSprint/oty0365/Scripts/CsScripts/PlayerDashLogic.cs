using System.Collections;
using UnityEngine;

public class PlayerSprintLogic
{
    private PlayerStatus _playerStatus;
    private Coroutine _currentSprintCoroutine;

    public PlayerSprintLogic(PlayerStatus playerStatus)
    {
        _playerStatus = playerStatus;
    }

    public void OnSprint(bool input)
    {
        
    }

    private IEnumerator SprintFlow()
    {
        if (_playerStatus.stamina.Value > 0)
        {
            _playerStatus.moveSpeed.Value = 3.5f;
        }
        while (_playerStatus.stamina.Value > 0)
        {
            _playerStatus.stamina.Value-=2*Time.deltaTime;
            yield return null;
        }
        _playerStatus.moveSpeed.Value = 2;
    }
    
}
