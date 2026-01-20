using UnityEngine;
using UnityEngine.Serialization;

public class PlayerConnector : MonoBehaviour, IConnector
{
    [Header("Controllers")]
    //[SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private MouseController mouseController;
    [SerializeField] private PlayerInteractionController playerInteractionController;
    [SerializeField] private PlayerGroundChecker playerGroundChecker;
    

    private PlayerInputController _playerInputController;
    private PlayerMoveController _playerMoveController;
    private PlayerStatus _playerStatus;
    private PlayerActionController _playerActionController;
    
    private IMover _moverInterface;
    private IForcer _forcerInterface;

    private void Start()
    {
        _playerStatus = GetComponent<PlayerStatus>();
        _playerMoveController = GetComponent<PlayerMoveController>();
        _playerInputController = GetComponent<PlayerInputController>();
        _playerActionController = GetComponent<PlayerActionController>();
        
        _moverInterface = GetComponent<IMover>();
        _forcerInterface = GetComponent<IForcer>();
        
        if (!ValidateReferences()) return;
        
        _playerMoveController.GetStatus(_playerStatus);
        _playerActionController.InitializeSprint(_playerStatus);
        _playerActionController.InitializeStamina(_playerStatus);
        mouseController.SetCamera(Camera.main);
        mouseController.SetPlayer(gameObject);
        OnConnect();
        
    }

    private bool ValidateReferences()
    {
        if (playerGroundChecker == null) { Debug.LogError("PlayerGroundChecker missing!"); return false; }
        if (_playerInputController == null) { Debug.LogError("PlayerInputController missing!"); return false; }
        if (mouseController == null) { Debug.LogError("MouseController missing!"); return false; }
        if (_playerMoveController == null) { Debug.LogError("PlayerMoveController missing!"); return false; }
        if(playerInteractionController == null) { Debug.LogError("PlayerInteractionController missing!"); return false; }
        if(_playerStatus == null) { Debug.LogError("PlayerStatus missing!"); return false; }
        if(_playerActionController == null) { Debug.LogError("PlayerActionController missing!"); return false; }
        
        if (_moverInterface == null) { Debug.LogError("IMover missing on this GameObject!"); return false; }
        if (_forcerInterface == null) { Debug.LogError("IForcer missing on this GameObject!"); return false; }

        return true;
    }

    public void OnConnect()
    {
        PlayerHfsm hfsm = new PlayerHfsm();
        hfsm.InitHfsm(gameObject);
        
        _playerInputController.EmbedHfsm(hfsm);
        playerGroundChecker.EmbedHfsm(hfsm);
        
        _playerActionController.PlayerSprintLogic.OnSprintStart += _playerActionController.PlayerStaminaLogic.StopReFill;
        _playerActionController.PlayerSprintLogic.OnSprintEnd+=_playerActionController.PlayerStaminaLogic.StartReFill;
        _playerActionController.PlayerSprintLogic.OnMoveSpeedUpdate+=_playerInputController.UpdateMove;
        
        _playerInputController.OnSprint+=_playerActionController.PlayerSprintLogic.OnSprint;
        _playerInputController.OnInteract += playerInteractionController.StartInteraction;
        _playerInputController.OnMove += _moverInterface.SetVelocity;
        _playerInputController.OnJump += _forcerInterface.SetAddForce;
        
        mouseController.OnMouseMove += _playerInputController.UpdateMove;
    }

    public void OnDisconnect()
    {
        if (_playerInputController != null)
        {
            if (_moverInterface != null)
                _playerInputController.OnMove -= _moverInterface.SetVelocity;
            
            if (_forcerInterface != null)
                _playerInputController.OnJump -= _forcerInterface.SetAddForce;
        }

        if (_playerInputController != null)
        {
            if (mouseController != null)
            {
                mouseController.OnMouseMove -= _playerInputController.UpdateMove;
            }

            if (playerInteractionController != null)
            {
                _playerInputController.OnInteract -= playerInteractionController.StartInteraction;
            }

            if (_playerActionController != null)
            {
                _playerInputController.OnSprint-=_playerActionController.PlayerSprintLogic.OnSprint;
                _playerActionController.PlayerSprintLogic.OnSprintStart -= _playerActionController.PlayerStaminaLogic.StopReFill;
                _playerActionController.PlayerSprintLogic.OnSprintEnd -=_playerActionController.PlayerStaminaLogic.StartReFill;
                _playerActionController.PlayerSprintLogic.OnMoveSpeedUpdate-=_playerInputController.UpdateMove;
            }
        }
    }

    private void OnDestroy() => OnDisconnect();
}

