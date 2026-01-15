using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class PlayerEyeCamera : MonoBehaviour
{
    [Header("Properties")] [SerializeField]
    private MinMaxRange<float> shakeX;
    [SerializeField]
    private MinMaxRange<float> shakeY;
    [SerializeField]
    private MinMaxRange<float> shakeZ;
    
    private Camera _camera;
    private Volume _volume;
    private Coroutine _currentShockFlow;

    public void GetShockedEffect()
    {
        if (_currentShockFlow != null)
        {
            StopCoroutine(_currentShockFlow);
        }

        _currentShockFlow = StartCoroutine(ShockFlow());
    }

    private IEnumerator ShockFlow()
    {
        yield return null;
    }
}
