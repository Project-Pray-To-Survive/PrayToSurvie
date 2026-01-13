using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConnector : MonoBehaviour,IConnector
{
    [SerializeField] private PlayerGroundChecker playerGroundChecker;
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private MouseController mouseController;
    [SerializeField] private PlayerMoveController playerMoveController;
    
    private IJumpState _jumpStateInterface;
    private IMoveState _moveStateInterface;
    private IMover _moverInterface;
    private IForcer _forcerInterface;
    
    private void Start()
    {
        _jumpStateInterface = gameObject.GetComponent<IJumpState>();
        _moverInterface = gameObject.GetComponent<IMover>();
        _forcerInterface = gameObject.GetComponent<IForcer>();
        _moveStateInterface = gameObject.GetComponent<IMoveState>();
        
        mouseController.SetCamera(Camera.main);
        mouseController.SetPlayer(gameObject);
        
        if (_moverInterface == null)
        {
            Debug.LogWarning("IMover is missing");
            return;
        }

        if (_forcerInterface == null)
        {
            Debug.LogWarning("IForcer is missing");
            return;
        }
        if (_jumpStateInterface == null)
        {
            Debug.LogError("IJumpState is missing");
            return;
        }
        OnConnect();
    }

    public void OnConnect()
    {
        playerGroundChecker.OnGroundEnter += _jumpStateInterface.EndJump;
        playerGroundChecker.OnGroundExit += _jumpStateInterface.StartJump;
        playerInputController.OnMove += _moverInterface.SetVelocity;
        playerInputController.OnJump += _forcerInterface.SetAddForce;
        mouseController.OnMouseMove += playerMoveController.UpdateVelocity;
        playerInputController.SetJumpStateInterface(_jumpStateInterface);
    }

    public void OnDisconnect()
    {
        if (_jumpStateInterface == null) return;
        if(_moverInterface == null) return;
        if(_forcerInterface == null) return;
        playerGroundChecker.OnGroundEnter -= _jumpStateInterface.EndJump;
        playerGroundChecker.OnGroundExit -= _jumpStateInterface.StartJump;
        playerInputController.OnMove -= _moverInterface.SetVelocity;
        playerInputController.OnJump -= _forcerInterface.SetAddForce;
    }
    public void OnDestroy()=>OnDisconnect();
}
