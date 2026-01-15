using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public event Action<Vector3> OnMove;
    public event Action<Vector3,ForceMode> OnJump;
    public event Action OnInteract;
    public event Action<bool> OnSprint;
    private IJumpState _jumpStateInterface;
    private Vector3 _moveDirection;
    

    public void SetJumpStateInterface(IJumpState jumpStateInterface)
    {
        _jumpStateInterface = jumpStateInterface;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        var dir = context.ReadValue<Vector2>();
        var dir3 =new Vector3(dir.x, 0, dir.y); 
        OnMove?.Invoke(dir3);
        _moveDirection = dir3;
    }

    public void UpdateMoveWhileDirChange()
    {
        OnMove?.Invoke(_moveDirection);
    }
    

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started&&!_jumpStateInterface.IsJumping)
        {
            OnJump?.Invoke(Vector3.up,ForceMode.Impulse);
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnSprint?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnSprint?.Invoke(false);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnInteract?.Invoke();
        }
    }
    
}
