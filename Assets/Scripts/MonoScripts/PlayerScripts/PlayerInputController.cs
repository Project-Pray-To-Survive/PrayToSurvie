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
    
    private FsmHandler _fsmHandler;
    private Hfsm _playerHfsm;
    private PlayerInputBuffer _playerInputBuffer ;
    private Vector3 _moveDirection;
    private Coroutine _checkMoveCoroutine;

    public void EmbedHfsm(FsmHandler fsmHandler,Hfsm playerHfsm)
    {
        _playerHfsm = playerHfsm;
        _fsmHandler = fsmHandler;
    }

    public void SetPlayerInputBuffer(PlayerInputBuffer playerInputBuffer)
    {
        _playerInputBuffer = playerInputBuffer;
    }

    private IEnumerator CheckMoveFlow()
    {
        while (true)
        {
            OnSprint?.Invoke(true);
            yield return null;
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerInputBuffer.SetFlag(PlayerInputFlags.Move,true);
            _fsmHandler.InsertToFsmQueue("Walk");
        }
        else if (context.canceled)
        {
            _playerInputBuffer.SetFlag(PlayerInputFlags.Move,false);
            _fsmHandler.InsertToFsmQueue("Idle");
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
            _playerInputBuffer.SetFlag(PlayerInputFlags.Sprint,true);
            _playerHfsm.ChangeState("Run");
            if (_checkMoveCoroutine != null)
            {
                StopCoroutine(_checkMoveCoroutine);
            }
            _checkMoveCoroutine = StartCoroutine(CheckMoveFlow());
        }
        else if (context.canceled)
        {
            _playerInputBuffer.SetFlag(PlayerInputFlags.Sprint,false);
            _playerHfsm.ChangeState("Idle");
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
