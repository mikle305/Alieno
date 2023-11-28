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
    [SerializeField] private GameObject _attackIndicator;

    private string _currentAnimation = _idle;

    public void UpdateAnimations(NavMeshAgent _agent)
    {
        if(_animator == null)
            return;
        
        string movingState = _agent.velocity.magnitude <= 0.01 ? _idle : _movingForward;

        if (_currentAnimation != movingState)
        {
            _currentAnimation = movingState;
            _animator.SetTrigger(_currentAnimation);
        }
    }

    public void SpawnAttackIndicator(Transform spawnPoint,float lifetime)
    {
        var effect = Instantiate(_attackIndicator, spawnPoint.position, Quaternion.identity);
        
        Destroy(effect,lifetime);
    }
}
