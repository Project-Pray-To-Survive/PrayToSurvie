using System;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    private FsmHandler _fsmHandler;

    public void EmbedHfsm(FsmHandler fsmHandler)
    {
        _fsmHandler = fsmHandler;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _fsmHandler.InsertToFsmQueue("OnGround");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _fsmHandler.InsertToFsmQueue("OnAir");
        }
    }
}
