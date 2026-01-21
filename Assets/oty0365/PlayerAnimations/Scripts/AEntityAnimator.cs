using System.Collections.Generic;
using UnityEngine;


public abstract class AEntityAnimator : MonoBehaviour
{
    [SerializeField] private Animator ani;

    public void SetAnimation(IAniState aniState)
    {
        aniState.SetState(ani);
    }

    public void SetSpeed(float speed)
    {
        ani.speed = speed;
    }
}
