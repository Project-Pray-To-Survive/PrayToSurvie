using System;
using UnityEngine;

public class TestItem : MonoBehaviour,IPassiveInteractable,IActiveInteractable
{
    public event Action OnActiveInteractExit;
    public void OnPassiveInteract()
    {
    }

    public void OnPassiveInteractExit()
    {
    }

    public void OnActiveInteract()
    {
        Debug.Log("OnActiveInteract");
        OnActiveInteractExit?.Invoke();
    }
    
}
