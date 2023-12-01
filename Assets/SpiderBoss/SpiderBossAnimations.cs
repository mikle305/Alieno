using System;
using UnityEngine;

public class SpiderBossAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _onLandingEffect;
    [SerializeField] private float _effctClearTime = 1f;
    
    public Action JumpAnimationFinished;
    public Action AttackAnimationFinished;

    public void InvokeJumpAnimationFinish()
    {
        JumpAnimationFinished?.Invoke();
        var effect = Instantiate(_onLandingEffect, transform.position, Quaternion.identity);
        Destroy(effect,_effctClearTime);
    }
    
    public void InvokeAttackAnimationFinish()
    {
        AttackAnimationFinished?.Invoke();
    }
}
