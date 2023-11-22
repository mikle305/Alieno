using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMainController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyRotation _enemyRotation;
    [SerializeField] private EnemyAnimations _enemyAnimations;

    private void Update()
    {
        _enemyMovement?.UpdateMovement(_navMeshAgent,_target);
        _enemyRotation?.UpdateRotation(_target);
        _enemyAnimations?.UpdateAnimations(_navMeshAgent);
    }

}
