using System;
using UnityEngine;

public class GrabableItem : ItemObject, IActiveInteractable,IGameObjectGetter
{
    [SerializeField] private Vector3 grabOffset = new(0, 0, 1.5f);
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform leftHandTransform;
    private Transform _holdPosition;
    private bool _isGrabbed;
    private GameObject _holdingPlayer;
    private Rigidbody _rigidbody;

    public event Action OnActiveInteractExit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Get(GameObject obj)
    {
        _holdingPlayer = obj;
    }

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

    private void Grab()
    {
        if (_holdingPlayer == null) return;
        _holdPosition = _holdingPlayer.transform;
        _isGrabbed = true;
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        var ppa = _holdingPlayer.GetComponent<PlayerProceduralAnimaiton>();
        ppa.SetLeftArmTarget(leftHandTransform);
        ppa.SetRightArmTarget(rightHandTransform);
        
        transform.SetParent(_holdPosition);
        transform.localPosition = grabOffset;
        transform.localRotation = Quaternion.identity;
    }

    private void Drop()
    {   
        var ppa = _holdingPlayer.GetComponent<PlayerProceduralAnimaiton>();
        ppa.FreeLeftArmTarget();
        ppa.FreeRightArmTarget();
        _isGrabbed = false;
        transform.SetParent(null);
        _holdPosition = null;
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }
        OnActiveInteractExit?.Invoke();
    }
}
