using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private IMover _moverInterface;

    private void Start()
    {
        _moverInterface = gameObject.GetComponent<IMover>();
        if (_moverInterface == null)
        {
            Debug.LogWarning("PlayerInputController: No IMover found");
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        var dir = context.ReadValue<Vector2>();
        var dir3 =new Vector3(dir.x, 0, dir.y); 
        _moverInterface.SetVelocity(dir3);
    }
    
}
