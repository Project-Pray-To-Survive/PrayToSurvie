using System;
using System.Collections;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    private FsmHandler _fsmHandler;
    private bool _onGround = true;
    
    public void EmbedHfsm(FsmHandler fsmHandler)
    {
        _fsmHandler = fsmHandler;
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f,
                LayerMask.GetMask("Ground")))
        {
            if (_onGround) return;
            _onGround = true;
            _fsmHandler.InsertToFsmQueue("OnGround");

        }
        else
        {
            if (!_onGround) return;
            _onGround = false;
            _fsmHandler.InsertToFsmQueue("OnAir");
        }

    }
}
