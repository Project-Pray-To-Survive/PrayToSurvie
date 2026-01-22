using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fsm
{
    private Dictionary<string, AState> _statDict = new();
    private AState _currentState;
    private string _currentStateName;

    public void AddState(string stateName, AState state)
    {
        _statDict[stateName] = state;
    }

    public bool HasState(string stateName)
    {
        return _statDict.ContainsKey(stateName);
    }

    public AState GetState(string stateName)
    {
        return _statDict[stateName];
    }

    public void RemoveState(string stateName)
    {
        _statDict.Remove(stateName);
    }

    public void ChangeState(string stateName)
    {
        if (_currentStateName == stateName) return;
        _currentState?.OnStateExit();
        _currentStateName = stateName;
        _currentState = GetState(stateName);
        _currentState?.OnStateEnter();
    }

    public bool IsState(string stateName)
    {
        return _currentStateName == stateName;
    }
}
