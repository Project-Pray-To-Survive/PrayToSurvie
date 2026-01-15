using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class PlayerEyeCamera : MonoBehaviour
{
    [Header("Shock Effect")] 
    [SerializeField] private float startSpeed = 2f;
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField] private float endSpeed = 1f;
    
    private Camera _camera;
    private Volume _volume;
    private Coroutine _currentShockCoroutine;
    private Coroutine _currentShakeCoroutine;
    private Vector3 _originalPosition;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _volume = GetComponent<Volume>();
        _originalPosition = transform.localPosition;
        
        if (_camera == null)
        {
            Debug.LogError($"[PlayerEyeCamera] No camera found on {gameObject.name}");
        }

        if (_volume == null)
        {
            Debug.LogError($"[PlayerEyeCamera] No volume found on {gameObject.name}");
        }
        //ShockedEffect();
        //ShakeCamera(0.3f,10f);
    }
    
    public void ShockedEffect()
    {
        if (_currentShockCoroutine != null)
        {
            StopCoroutine(_currentShockCoroutine);
        }

        _currentShockCoroutine = StartCoroutine(ShockFlow());
    }

    public void ShakeCamera(float shakeIntensity, float shakeDuration)
    {
        if (_currentShakeCoroutine != null)
        {
            StopCoroutine(_currentShakeCoroutine);
        }

        _currentShakeCoroutine = StartCoroutine(ShakeFlow(shakeIntensity, shakeDuration));
    }

    private IEnumerator ShakeFlow(float shakeIntensity, float shakeDuration)
    {
        float elapsed = 0f;
        
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;
            float z = Random.Range(-1f, 1f) * shakeIntensity;
            
            var newPosition = _originalPosition + new Vector3(x, y, z);
            while (Vector3.Distance(transform.position, newPosition) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10f);
            }
            
            elapsed += Time.deltaTime;
            yield return null;
        }


        while (Vector3.Distance(transform.position, _originalPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, _originalPosition, Time.deltaTime * 10f);
        }
        _currentShakeCoroutine = null;
    }

    private IEnumerator ShockFlow()
    {
        for (float t = 0f; t <= 1f; t += startSpeed * Time.deltaTime)
        {
            _volume.weight = Mathf.Clamp01(t);
            yield return null;
        }
        _volume.weight = 1f;
        
        yield return new WaitForSeconds(waitTime);
        
        for (float t = 1f; t >= 0f; t -= endSpeed * Time.deltaTime)
        {
            _volume.weight = Mathf.Clamp01(t);
            yield return null;
        }
        _volume.weight = 0f;
        
        _currentShockCoroutine = null;
    }
}