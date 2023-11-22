using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : MonoBehaviour
{
    private static string _idle = "Idle";
    private static string _movingForward = "MovingForward";
    
    [SerializeField] private Animator _animator;

    private string _currentAnimation = _idle;

    public void UpdateAnimations(NavMeshAgent _agent)
    {
        string movingState = _agent.velocity.magnitude <= 0.01 ? _idle : _movingForward;

        if (_currentAnimation != movingState)
        {
            _currentAnimation = movingState;
            _animator.SetTrigger(_currentAnimation);
        }
    }
}
