using UnityEngine;

public class PlayerMoveController : MonoBehaviour,IMover
{
    [SerializeField] private Rigidbody rb;

    public void SetPosition(Vector3 pos)
    {
        rb.position = pos;
    }

    public void SetVelocity(Vector3 vel)
    {
        rb.linearVelocity = vel;
    }
}
