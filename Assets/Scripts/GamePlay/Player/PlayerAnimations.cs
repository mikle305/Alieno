using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private static string _idle = "Idle";
    private static string _rightAttack = "RightAttack";
    private static string _leftAttack = "LeftAttack";
    private static string _attackSpeed = "AttackSpeed";

    private string _lastAttackUsed = _leftAttack;
    public event Action OnAttackAnimation; 
    public void PlayAttackAnimation(float attackSpeed)
    {
        _animator.SetFloat(_attackSpeed,1/attackSpeed);

        string newAttackUsed = _lastAttackUsed == _leftAttack ? _rightAttack : _leftAttack;
        _animator.SetTrigger(newAttackUsed);
        _lastAttackUsed = newAttackUsed;
    }

    public void InvokeAttack()
    {
        OnAttackAnimation?.Invoke();
    }
}
