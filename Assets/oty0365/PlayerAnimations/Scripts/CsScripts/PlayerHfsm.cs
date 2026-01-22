using UnityEngine;

public class PlayerHfsm : Hfsm
{
    private GameObject _parent;
    private Fsm _playerFsm = new Fsm();
    private Fsm _playerGroundFsm = new Fsm();

    public override void InitHfsm(GameObject parent)
    {
        _parent = parent;
        _playerFsm = new Fsm();
        _playerGroundFsm = new Fsm();
        _playerFsm.AddState("Walk", new PlayerWalkState(_parent,_playerFsm));
        _playerFsm.AddState("Idle", new PlayerIdleState(_parent, _playerFsm));
        _playerFsm.AddState("Run", new PlayerRunState(_parent, _playerFsm));
        _playerGroundFsm.AddState("OnGround",new PlayerOnGroundState(_parent, _playerGroundFsm));
        _playerGroundFsm.AddState("OnAir",new PlayerOnAirState(_parent, _playerGroundFsm));
        _playerFsm.ChangeState("Idle");
        _playerGroundFsm.ChangeState("OnGround");
    }
    public override void ChangeState(string stateName)
    {
        if (_playerFsm.HasState(stateName))
        { 
            _playerFsm.ChangeState(stateName);
        }
        else if (_playerGroundFsm.HasState(stateName))
        {
            _playerGroundFsm.ChangeState(stateName);
        }
    }

    public override bool IsState(string stateName)
    {
        return _playerFsm.IsState(stateName) || _playerGroundFsm.IsState(stateName);
    }
}
