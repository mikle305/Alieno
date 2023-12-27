using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Ai/Soldier AI")]
    public class BasicSoldierAI : EnemyAI
    {
        public override void Execute(
            NavMeshAgent navMeshAgent, 
            EnemyMovement enemyMovement,
            EnemyRotation enemyRotation,
            EnemyAnimations enemyAnimations, 
            EnemyAttacker enemyAttacker)
        {
            enemyRotation?.UpdateRotation(Target);
            enemyAnimations?.UpdateAnimations(navMeshAgent);
            bool isVisible = GameplayUtils.IsVisible(enemyRotation.transform, Target);
            if (!enemyAttacker.OnCooldown && isVisible)
            {
                enemyMovement?.UpdateMovement(navMeshAgent, navMeshAgent.transform);

                enemyAttacker.Attack();
            }
            else if (!isVisible)
            {
                enemyMovement?.UpdateMovement(navMeshAgent, Target);
            }
        }
    }
}