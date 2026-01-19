
public interface IMoveState
{
    public bool IsMoving { get; }
    public void StartMove();
    public void EndMove();
}