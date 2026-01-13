using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private IMover _moverInterface;
    private IForcer _forcerInterface;

    private void Start()
    {
        _moverInterface = gameObject.GetComponent<IMover>();
        _forcerInterface = gameObject.GetComponent<IForcer>();
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

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _forcerInterface.SetAddForce(new Vector3(0, 4, 0), ForceMode.Impulse);
        }
    }
    
}
