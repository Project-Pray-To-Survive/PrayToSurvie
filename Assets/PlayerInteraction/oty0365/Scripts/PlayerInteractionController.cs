using System;
using System.Collections;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Properties")] 
    [SerializeField] private float interactionRange;
    [SerializeField] private float checkInterval;
    
    private Coroutine _checkInteractionCoroutine;
    private IPassiveInteractable _passiveInteractable;
    private IActiveInteractable _activeInteractable;
    

    private IEnumerator CheckInteraction()
    {
        while (true)
        {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionRange);
            var curPassiveInteractable = hit.collider?.GetComponent<IPassiveInteractable>();
            if (_passiveInteractable != curPassiveInteractable)
            {
                _passiveInteractable?.OnPassiveInteractExit();
                _passiveInteractable = curPassiveInteractable;
                _passiveInteractable?.OnPassiveInteract();
            }

            var curActiveInteractable = hit.collider?.GetComponent<IActiveInteractable>();
            if (curActiveInteractable == null)
            {
                _activeInteractable = null;
            }
            else if (_activeInteractable != curActiveInteractable)
            {
                if (_activeInteractable != null)
                {
                    _activeInteractable.OnActiveInteractExit -= StopInteraction;
                }

                _activeInteractable = curActiveInteractable;
                if (_activeInteractable != null)
                {
                    _activeInteractable.OnActiveInteractExit += StopInteraction;
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    public void StartInteraction()
    {
        if (_activeInteractable == null) return;
        if (_checkInteractionCoroutine != null)
        {
            StopCoroutine(_checkInteractionCoroutine);
            _checkInteractionCoroutine = null;
        }
        _activeInteractable?.OnActiveInteract();
    }

    public void StopInteraction()
    {
        if (_checkInteractionCoroutine != null)
        {
           _checkInteractionCoroutine = StartCoroutine(CheckInteraction());
        }
    }

    private void Start()
    {
        _checkInteractionCoroutine = StartCoroutine(CheckInteraction());
    }
}
