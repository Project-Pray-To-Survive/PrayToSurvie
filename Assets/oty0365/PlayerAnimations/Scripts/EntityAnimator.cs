using System.Collections.Generic;
using UnityEngine;


public abstract class EntityAnimator : MonoBehaviour
{
    [SerializeField] private Animator ani;

    public void SetAnimation(IAniState aniState)
    {
        aniState.SetState(ani);
    }
}
