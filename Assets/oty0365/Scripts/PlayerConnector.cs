using UnityEngine;

public class PlayerConnector : MonoBehaviour, IConnector
{
    [Header("Controllers")]
    [SerializeField] private PlayerGroundChecker playerGroundChecker;
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private MouseController mouseController;
    [SerializeField] private PlayerMoveController playerMoveController;
    [SerializeField] private PlayerInteractionController playerInteractionController;

    private IJumpState _jumpStateInterface;
    private IMover _moverInterface;
    private IForcer _forcerInterface;

    private void Start()
    {
        _jumpStateInterface = GetComponent<IJumpState>();
        _moverInterface = GetComponent<IMover>();
        _forcerInterface = GetComponent<IForcer>();
        
        if (!ValidateReferences()) return;
        
        mouseController.SetCamera(Camera.main);
        mouseController.SetPlayer(gameObject);
        playerInputController.SetJumpStateInterface(_jumpStateInterface);

        OnConnect();
    }

    private bool ValidateReferences()
    {
        if (playerGroundChecker == null) { Debug.LogError("PlayerGroundChecker missing!"); return false; }
        if (playerInputController == null) { Debug.LogError("PlayerInputController missing!"); return false; }
        if (mouseController == null) { Debug.LogError("MouseController missing!"); return false; }
        if (playerMoveController == null) { Debug.LogError("PlayerMoveController missing!"); return false; }
        if(playerInteractionController == null) { Debug.LogError("PlayerInteractionController missing!"); return false; }
        
        if (_jumpStateInterface == null) { Debug.LogError("IJumpState missing on this GameObject!"); return false; }
        if (_moverInterface == null) { Debug.LogError("IMover missing on this GameObject!"); return false; }
        if (_forcerInterface == null) { Debug.LogError("IForcer missing on this GameObject!"); return false; }

        return true;
    }

    public void OnConnect()
    {
        playerGroundChecker.OnGroundEnter += _jumpStateInterface.EndJump;
        playerGroundChecker.OnGroundEnter += playerMoveController.SetOnGround;
        playerGroundChecker.OnGroundExit += _jumpStateInterface.StartJump;
        playerInputController.OnInteract += playerInteractionController.StartInteraction;
        playerInputController.OnMove += _moverInterface.SetVelocity;
        playerInputController.OnJump += _forcerInterface.SetAddForce;
        mouseController.OnMouseMove += playerInputController.UpdateMoveWhileDirChange;
    }

    public void OnDisconnect()
    {
        if (playerGroundChecker != null && _jumpStateInterface != null)
        {
            playerGroundChecker.OnGroundEnter -= _jumpStateInterface.EndJump;
            playerGroundChecker.OnGroundExit -= _jumpStateInterface.StartJump;
        }

        if (playerInputController != null)
        {
            playerGroundChecker.OnGroundEnter -= playerMoveController.SetOnGround;
        }

        if (playerInputController != null)
        {
            if (_moverInterface != null)
                playerInputController.OnMove -= _moverInterface.SetVelocity;
            
            if (_forcerInterface != null)
                playerInputController.OnJump -= _forcerInterface.SetAddForce;
        }

        if (playerInputController != null)
        {
            if (mouseController != null)
            {
                mouseController.OnMouseMove -= playerInputController.UpdateMoveWhileDirChange;
            }

            if (playerInteractionController != null)
            {
                playerInputController.OnInteract -= playerInteractionController.StartInteraction;
            }
        }
    }

    private void OnDestroy() => OnDisconnect();
}

