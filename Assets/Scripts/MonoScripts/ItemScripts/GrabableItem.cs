using System;
using UnityEngine;

public class GrabableItem : ItemObject, IActiveInteractable,IGameObjectGetter
{
    [SerializeField] private Vector3 grabOffset = new(0, 0, 1.5f);
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Rigidbody _rigidbody;
    
    private PlayerHand _playerInventory;
    private Transform _holdPosition;
    private bool _isGrabbed;
    public bool IsGrabbed=>_isGrabbed;
    public Transform RightHandTransform => rightHandTransform;
    public Transform LeftHandTransform => leftHandTransform;
    public Vector3 GrabbOffset => grabOffset;
    public event Action OnActiveInteractExit;

    public void OnActiveInteract()
    {
        if (!_isGrabbed)
        {
            Grab();
        }
        else
        {
            Drop();
        }
    }
    public virtual void Get(GameObject obj)
    {
        _playerInventory = obj.GetComponent<PlayerHand>();
        _playerInventory?.Observe(this);
    }

    protected virtual void Grab()
    {
        _isGrabbed = true;
        _playerInventory?.Put();
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        
    }

    protected virtual void Drop()
    {  
        _playerInventory?.Release();
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _isGrabbed = false;
        OnActiveInteractExit?.Invoke();
    }
}
