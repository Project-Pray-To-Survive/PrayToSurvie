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
    
    private Hfsm _playerHfsm;
    private Vector3 _moveDirection;
    private Coroutine _checkMoveCoroutine;

    public void EmbedHfsm(Hfsm fsm)
    {
        _playerHfsm = fsm;
    }
    
    private IEnumerator CheckMoveFlow()
    {
        while (true)
        {
            OnSprint?.Invoke(_playerHfsm.IsState("Walk"));
            yield return null;
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerHfsm.ChangeState("Walk");
        }
        else if (context.canceled)
        {
            _playerHfsm.ChangeState("Idle");
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
        if (context.started&&_playerHfsm.IsState("OnGround"))
        {
            OnJump?.Invoke(Vector3.up,ForceMode.Impulse);
            _playerHfsm.ChangeState("Air");
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
