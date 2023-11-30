using System.Collections;
using System.Collections.Generic;
using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy Ai/SmartDrone AI")]
public class SmartDroneAI : EnemyAI
{
    [SerializeField] private float _delayBeforeAttack = 0.75f;

    private Vector3 _futureEnemyPos;
    public override void Execute(NavMeshAgent _navMeshAgent, EnemyMovement _enemyMovement, EnemyRotation _enemyRotation,
        EnemyAnimations _enemyAnimations,EnemyAttacker _enemyAttacker)
    {
        _futureEnemyPos = GameplayUtils.CalcFuturePos(Target, TargetRigidbody, 0.75f);

        if (!_enemyAttacker.OnCooldown)
        {
            _navMeshAgent.enabled = true;
            _enemyMovement?.UpdateMovement(_navMeshAgent, _futureEnemyPos);
            _enemyRotation?.UpdateRotation(_futureEnemyPos);
            if (GameplayUtils.DistanceBetween(_navMeshAgent.transform, Target) <= 3f || GameplayUtils.DistanceBetween(_navMeshAgent.transform.position, _futureEnemyPos) <= 1)
            {
                _navMeshAgent.enabled = false;

                _enemyAnimations?.SpawnAttackIndicator(_navMeshAgent.transform,_delayBeforeAttack*2);
                _enemyAttacker?.AttackWithDelay(_delayBeforeAttack);
            }
        }
    }
}