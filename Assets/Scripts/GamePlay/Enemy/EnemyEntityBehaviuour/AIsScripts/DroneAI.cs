using System.Collections;
using System.Collections.Generic;
using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy Ai/Drone AI")]
public class DroneAI : EnemyAI
{
    [SerializeField] private float _delayBeforeAttack = 0.5f;
    public override void Execute(NavMeshAgent _navMeshAgent, EnemyMovement _enemyMovement, EnemyRotation _enemyRotation,
        EnemyAnimations _enemyAnimations,EnemyAttacker _enemyAttacker)
    {

        if (!_enemyAttacker.OnCooldown)
        {
            _navMeshAgent.enabled = true;
            _enemyMovement?.UpdateMovement(_navMeshAgent, Target);
            _enemyRotation?.UpdateRotation(Target);
            if (GameplayUtils.DistanceBetween(_navMeshAgent.transform, Target) <= 3f)
            {
                _navMeshAgent.enabled = false;

                _enemyAnimations?.SpawnAttackIndicator(_navMeshAgent.transform,_delayBeforeAttack*2);
                _enemyAttacker?.AttackWithDelay(_delayBeforeAttack);
            }
        }
    }
}
