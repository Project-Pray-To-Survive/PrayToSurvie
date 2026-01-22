using UnityEngine;

public abstract class Hfsm
{
    public abstract void InitHfsm(GameObject parent);
    public abstract void ChangeState(string stateName);
    
    public abstract bool IsState(string stateName);
}
