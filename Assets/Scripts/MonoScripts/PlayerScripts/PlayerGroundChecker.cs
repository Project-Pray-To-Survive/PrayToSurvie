using System;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    private Hfsm _playerHfsm;

    public void EmbedHfsm(Hfsm playerHfsm)
    {
        _playerHfsm = playerHfsm;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _playerHfsm.ChangeState("OnGround");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _playerHfsm.ChangeState("OnAir");
        }
    }
}
