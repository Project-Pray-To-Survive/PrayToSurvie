using System.Collections.Generic;
using UnityEngine;

public class FsmHandler : MonoBehaviour
{
    private Hfsm _hfsm;
    private List<string> _cmdList = new List<string>();
    private HashSet<string> _cmdSet = new HashSet<string>();

    public void EmbedHfsm(Hfsm hfsm)
    {
        _hfsm = hfsm;
    }
    
    public void InsertToFsmQueue(string cmd)
    {
        if (_cmdSet.Contains(cmd))
        {
            _cmdList.Remove(cmd);
            _cmdList.Add(cmd);
        }
        else
        {
            _cmdList.Add(cmd);
            _cmdSet.Add(cmd);
        }
    }

    public void RemoveFromFsmQueue(string cmd)
    {
        if (!_cmdList.Contains(cmd)) return;
        _cmdList.Remove(cmd);
        _cmdSet.Remove(cmd);
    }

    public void Update()
    {
        if (_cmdList.Count == 0) return;

        var curState = _cmdList[^1];
        _cmdList.Clear();
        _cmdSet.Clear();

        _hfsm.ChangeState(curState);
    }
    
}
