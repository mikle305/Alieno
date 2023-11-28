using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : ScriptableObject
{
    public Transform Target { get; set; }
    public abstract void Execute(NavMeshAgent _navMeshAgent,EnemyMovement _enemyMovement,EnemyRotation _enemyRotation,EnemyAnimations _enemyAnimations,EnemyAttacker enemyAttacker);
}
