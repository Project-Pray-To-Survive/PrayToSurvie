using System;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour,IMover,IForcer
{
    [Header("Heart Components")]
    [SerializeField] private Rigidbody rb;
    private PlayerStatus _playerStatus;
    
    
    public void SetPosition(Vector3 pos)
    {
        rb.position = pos;
    }

    public void GetStatus(PlayerStatus status)
    {
        _playerStatus = status;
    }

    public void SetVelocity(Vector3 input)
    {
        Vector3 moveDir = transform.forward * input.z + transform.right * input.x;
        float currentYVelocity = rb.linearVelocity.y; 
        Vector3 horizontalVelocity = moveDir * _playerStatus.moveSpeed.Value;
        
        Vector3 finalVelocity = new Vector3(horizontalVelocity.x, currentYVelocity, horizontalVelocity.z);
    
        rb.linearVelocity = finalVelocity;
        
    }

    public void SetOnGround()
    {
        SetVelocity(Vector3.zero);
    }

    public void SetAddForce(Vector3 force, ForceMode mode)
    {
        if (force.y > 0) 
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        }
    
        rb.AddForce(force.normalized * _playerStatus.jumpSpeed.Value, mode);
    }
}
