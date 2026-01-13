using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public event Action<Vector3> OnMove;
    public event Action<Vector3,ForceMode> OnJump;
    private IJumpState _jumpStateInterface;
    private IMoveState _moveStateInterface;
    

    public void SetJumpStateInterface(IJumpState jumpStateInterface)
    {
        _jumpStateInterface = jumpStateInterface;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _moveStateInterface.StartMove();
            var dir = context.ReadValue<Vector2>();
            var dir3 =new Vector3(dir.x, 0, dir.y); 
            OnMove?.Invoke(dir3);
        }
        else if (context.canceled)
        {
            _moveStateInterface.EndMove();
        }
    }
    

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started&&!_jumpStateInterface.IsJumping)
        {
            OnJump?.Invoke(new Vector3(0,4,0),ForceMode.Impulse);
        }
    }
    
}
