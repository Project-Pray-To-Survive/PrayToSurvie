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
    private IMoveState _moveStateInterface;
    private Vector3 _moveDirection;
    private Coroutine _checkMoveCoroutine;

    private IEnumerator CheckMoveFlow()
    {
        while (true)
        {
            OnSprint?.Invoke(_moveStateInterface.IsMoving);
            yield return null;
        }
    }

    public void SetJumpStateInterface(IJumpState jumpStateInterface)
    {
        _jumpStateInterface = jumpStateInterface;
    }

    public void SetMoveStateInterface(IMoveState moveStateInterface)
    {
        _moveStateInterface = moveStateInterface;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _moveStateInterface.StartMove();
        }
        else if (context.canceled)
        {
            _moveStateInterface.EndMove();
        }
        var dir = context.ReadValue<Vector2>();
        var dir3 =new Vector3(dir.x, 0, dir.y); 
        OnMove?.Invoke(dir3);
        _moveDirection = dir3;
    }

    public void UpdateMove()
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
            if (_checkMoveCoroutine != null)
            {
                StopCoroutine(_checkMoveCoroutine);
            }
            _checkMoveCoroutine = StartCoroutine(CheckMoveFlow());
        }
        else if (context.canceled)
        {
            if (_checkMoveCoroutine != null)
            {
                StopCoroutine(_checkMoveCoroutine);
                _checkMoveCoroutine = null;
            }
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
