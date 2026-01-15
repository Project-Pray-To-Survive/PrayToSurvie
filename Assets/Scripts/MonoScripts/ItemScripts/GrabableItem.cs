using System;
using UnityEngine;

public class GrabableItem : ItemObject, IActiveInteractable, IPassiveInteractable
{
    [SerializeField] private Vector3 grabOffset = new(0, 0, 1.5f);
    [SerializeField] private GameObject player;
    private Transform _holdPosition;
    private bool _isGrabbed;
    private Rigidbody _rigidbody;

    public event Action OnActiveInteractExit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        if (player == null) return;
        _holdPosition = player.transform;
        _isGrabbed = true;
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        // 들고 다닐 때의 오프셋 설정
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

    public void OnPassiveInteract()
    {
        // 상호작용 가능함을 표시 (예: 하이라이트)
    }

    public void OnPassiveInteractExit()
    {
        // 하이라이트 해제
    }
}
