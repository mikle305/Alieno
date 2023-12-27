using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Ai/Drone AI")]
    public class DroneAI : EnemyAI
    {
        [SerializeField] private float _delayBeforeAttack = 0.5f;

        
        public override void Execute(
            NavMeshAgent navMeshAgent, 
            EnemyMovement enemyMovement,
            EnemyRotation enemyRotation,
            EnemyAnimations enemyAnimations, 
            EnemyAttacker enemyAttacker)
        {
            if (enemyAttacker.OnCooldown) 
                return;
            
            navMeshAgent.enabled = true;
            enemyMovement?.UpdateMovement(navMeshAgent, Target);
            enemyRotation?.UpdateRotation(Target);
            if (!(GameplayUtils.DistanceBetween(navMeshAgent.transform, Target) <= 3f)) 
                return;
            
            navMeshAgent.enabled = false;
            enemyAnimations?.SpawnAttackIndicator(navMeshAgent.transform, _delayBeforeAttack * 2);
            enemyAttacker.AttackWithDelay(_delayBeforeAttack);
        }
    }
}