using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Ai/EliteSoldier AI")]
    public class EliteSoldierAI : EnemyAI
    {
        [SerializeField] private float _delayBeforeAttack;

        [SerializeField] private Vector3 _attackIndicatorOffset = new(0, 0.2f, 0);

        private Vector3 _shootingPos;

        
        public override void Execute(
            NavMeshAgent navMeshAgent, 
            EnemyMovement enemyMovement,
            EnemyRotation enemyRotation,
            EnemyAnimations enemyAnimations, 
            EnemyAttacker enemyAttacker)
        {
            enemyAnimations?.UpdateAnimations(navMeshAgent);

            if (!enemyAttacker.OnCooldown)
            {
                _shootingPos = GameplayUtils.PredictPosition(Target, TargetRigidbody, _delayBeforeAttack * 1.05f);
                enemyAnimations?.SpawnAttackIndicatorTowards(navMeshAgent.transform.position + _attackIndicatorOffset,
                    _delayBeforeAttack * 1.4f, _shootingPos);
                enemyRotation?.UpdateRotation(_shootingPos);
                enemyAttacker?.AttackWithDelay(_delayBeforeAttack);
            }
            else
            {
                enemyRotation?.UpdateRotation(_shootingPos);
            }
        }
    }
}