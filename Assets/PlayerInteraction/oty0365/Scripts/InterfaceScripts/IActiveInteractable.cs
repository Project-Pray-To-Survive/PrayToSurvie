using System;
using UnityEngine;

public interface IActiveInteractable
{
    public void OnActiveInteract();
    public event Action OnActiveInteractExit;
}
