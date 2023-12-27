using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Ai/SmartDrone AI")]
    public class SmartDroneAI : EnemyAI
    {
        [SerializeField] private float _delayBeforeAttack = 0.75f;

        private Vector3 _futureEnemyPos;

        public override void Execute(
            NavMeshAgent navMeshAgent, 
            EnemyMovement enemyMovement,
            EnemyRotation enemyRotation,
            EnemyAnimations enemyAnimations, 
            EnemyAttacker enemyAttacker)
        {
            _futureEnemyPos = GameplayUtils.PredictPosition(Target, TargetRigidbody, 0.75f);

            if (enemyAttacker.OnCooldown) 
                return;
            
            navMeshAgent.enabled = true;
            enemyMovement?.UpdateMovement(navMeshAgent, _futureEnemyPos);
            enemyRotation?.UpdateRotation(_futureEnemyPos);
            if (GameplayUtils.DistanceBetween(navMeshAgent.transform, Target) <= 3f ||
                GameplayUtils.DistanceBetween(navMeshAgent.transform.position, _futureEnemyPos) <= 1)
            {
                navMeshAgent.enabled = false;

                enemyAnimations?.SpawnAttackIndicator(navMeshAgent.transform, _delayBeforeAttack * 2);
                enemyAttacker.AttackWithDelay(_delayBeforeAttack);
            }
        }
    }
}