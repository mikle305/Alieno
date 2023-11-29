using System.Collections;
using System.Collections.Generic;
using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy Ai/RocketTurret AI")]
public class RocketTurretAI : EnemyAI
{
    public override void Execute(NavMeshAgent _navMeshAgent, EnemyMovement _enemyMovement, EnemyRotation _enemyRotation,
        EnemyAnimations _enemyAnimations,EnemyAttacker _enemyAttacker)
    {
        _enemyRotation?.UpdateRotation(Target);
        if (!_enemyAttacker.OnCooldown)
            _enemyAttacker?.Attack();
    }
}