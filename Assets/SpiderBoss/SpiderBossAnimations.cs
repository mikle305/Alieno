using System;
using UnityEngine;

public class SpiderBossAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public Action JumpAnimationFinished;
    public Action AttackAnimationFinished;

    public void InvokeJumpAnimationFinish()
    {
        JumpAnimationFinished?.Invoke();
    }
    
    public void InvokeAttackAnimationFinish()
    {
        AttackAnimationFinished?.Invoke();
    }
}
