using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInteractionController : MonoBehaviour
{
    public event Action<bool> OnButtonSet;
    [Header("Properties")] 
    [SerializeField] private float interactionRange;
    [SerializeField] private float checkInterval;
    
    private Coroutine _checkInteractionCoroutine;
    private IPassiveInteractable _passiveInteractable;
    private IActiveInteractable _activeInteractable;
    private IGameObjectGetter _gameObjectGetter;
    
    

    private IEnumerator CheckInteraction()
    {
        while (true)
        {
            //레이캐스트로 감지
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionRange);

            var curPassiveInteractable = hit.collider?.GetComponent<IPassiveInteractable>();
            //패시브 인터렉터블 교체
            if (_passiveInteractable != curPassiveInteractable)
            {
                _passiveInteractable?.OnPassiveInteractExit();
                _passiveInteractable = curPassiveInteractable;
                _passiveInteractable?.OnPassiveInteract();
            }
            var curPlayerGetter = hit.collider?.GetComponent<IGameObjectGetter>();
            if (_gameObjectGetter != curPlayerGetter)
            {
                _gameObjectGetter = curPlayerGetter;
                _gameObjectGetter?.Get(gameObject.transform.parent.gameObject);
            }

            var curActiveInteractable = hit.collider?.GetComponent<IActiveInteractable>();
            if (curActiveInteractable == null)
            {
                _activeInteractable = null;
                OnButtonSet?.Invoke(false);
                
            }
            else
            {
                OnButtonSet?.Invoke(true);
                if (_activeInteractable != curActiveInteractable)
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
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    public void StartInteraction()
    {
        
        if (_activeInteractable == null) return;
        OnButtonSet?.Invoke(false);
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
            StopCoroutine(_checkInteractionCoroutine);
        }
        OnButtonSet?.Invoke(true);
        _checkInteractionCoroutine = StartCoroutine(CheckInteraction());
    }

    private void Start()
    {
        _checkInteractionCoroutine = StartCoroutine(CheckInteraction());
    }
}
