using System;
using UnityEngine;

public class TestItem : MonoBehaviour,IPassiveInteractable,IActiveInteractable
{
    public event Action OnActiveInteractExit;
    public void OnPassiveInteract()
    {
        Debug.Log("OnPassiveInteract");
    }

    public void OnPassiveInteractExit()
    {
        Debug.Log("OnPassiveInteractExit");
    }

    public void OnActiveInteract()
    {
        Debug.Log("OnActiveInteract");
    }
    
}
