using UnityEngine;

public class PlayerBehavior : MonoBehaviour,IJumpState,IMoveState,ISprintState
{
    public bool IsMoving {get; private set;}
    public bool IsJumping { get; private set; }
    public bool IsSprinting { get; private set; }
    public void StartJump() => IsJumping = true;
    public void EndJump() => IsJumping = false;
    public void StartMove() => IsMoving = true;
    public void EndMove() => IsMoving = false;

    public void SetSprint(bool isSprinting)
    {
        IsSprinting = isSprinting;
    }
}
