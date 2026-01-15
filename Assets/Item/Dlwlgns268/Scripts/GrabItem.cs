using System;
using UnityEngine;

public class GrabItem : MonoBehaviour, IActiveInteractable, IPassiveInteractable
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
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // 플레이어 카메라를 기준으로 약간 앞쪽에 들고 있도록 설정
            _holdPosition = Camera.main.transform;
            
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
