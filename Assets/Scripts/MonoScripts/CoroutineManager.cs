using System.Collections;
using UnityEngine;

public class CoroutineManager : SceneSingletonMonoBehavior<CoroutineManager>
{
    public Coroutine StartRoutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
    public void StopRoutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}
