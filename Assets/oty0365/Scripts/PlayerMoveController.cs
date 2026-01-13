using System;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour,IMover,IForcer
{
    [SerializeField] private Rigidbody rb;
    public void SetPosition(Vector3 pos)
    {
        rb.position = pos;
    }

    public void SetVelocity(Vector3 input)
    {
        
        Vector3 moveDir = transform.forward * input.z + transform.right * input.x;
        
        if (moveDir.sqrMagnitude > 1f)
            moveDir.Normalize();
        
        moveDir.y = rb.linearVelocity.y;
        rb.linearVelocity = moveDir;
    }

    public void SetAddForce(Vector3 force,ForceMode mode)
    {
        rb.AddForce(force,ForceMode.Impulse);
    }
}
