using System;
using UnityEngine;

public class TestItem : MonoBehaviour,IPassiveInteractable,IActiveInteractable
{
    [SerializeField] private Vector3 grabOffset = new Vector3(0, 0, 1.5f);
    private Transform _holdPosition;
    private bool _isGrabbed = false;
    private Rigidbody _rigidbody;

    public event Action OnActiveInteractExit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPassiveInteract()
    {
    }

    public void OnPassiveInteractExit()
    {
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
        _holdPosition = Camera.main.transform;
        _isGrabbed = true;

        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        transform.SetParent(_holdPosition);
        transform.localPosition = grabOffset;
        transform.localRotation = Quaternion.identity;
    }

    private void Drop()
    {
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
