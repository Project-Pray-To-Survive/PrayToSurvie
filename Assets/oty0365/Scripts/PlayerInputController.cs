using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }
    
}
