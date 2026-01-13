using System;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    public event Action onGroundEnter;
    public event Action onGroundExit;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On");
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            onGroundEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Off");
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            onGroundExit?.Invoke();
        }
    }
}
