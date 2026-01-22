using UnityEngine;

public abstract class AState
{
    protected GameObject _parent;
    protected Fsm _fsm;

    public AState(GameObject parent, Fsm fsm)
    {
        _fsm = fsm;
        _parent = parent;
    }
    
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
}
