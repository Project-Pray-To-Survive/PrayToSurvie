
public interface IJumpState
{
    public bool IsJumping { get; }
    public void StartJump();
    public void EndJump();
}