using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public event Action OnMouseMove;
    
    [SerializeField] private float mouseSensitivity = 100f;
    private Camera _playerCamera;
    private GameObject _player;
    
    private float _cameraPitch = 0f;
    private float _cameraYaw = 0f;
    private Vector2 _prevMouseData = Vector2.zero;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetCamera(Camera playerCamera)
    {
        _playerCamera = playerCamera;
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
    
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            if (mouseDelta != _prevMouseData)
            {
                OnMouseMove?.Invoke();
                _prevMouseData = mouseDelta;
            }
            
            float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;
            
            _cameraPitch -= mouseY;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -90f, 90f);
            _cameraYaw += mouseX;
            _playerCamera.transform.localRotation = Quaternion.Euler(_cameraPitch,0, 0);
            _player.transform.rotation = Quaternion.Euler(0, _cameraYaw, 0);
        }
    }
}