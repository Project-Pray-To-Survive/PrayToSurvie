using System;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    public event Action OnGroundEnter;
    public event Action OnGroundExit;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            OnGroundEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            OnGroundExit?.Invoke();
        }
    }
}
