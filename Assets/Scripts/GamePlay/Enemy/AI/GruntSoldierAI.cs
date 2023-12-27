using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Ai/Grunt AI")]
    public class GruntSoldierAI : EnemyAI
    {
        public override void Execute(
            NavMeshAgent navMeshAgent, 
            EnemyMovement enemyMovement, 
            EnemyRotation enemyRotation,
            EnemyAnimations enemyAnimations,
            EnemyAttacker enemyAttacker)
        {
            bool isVisible = GameplayUtils.IsVisible(enemyRotation.transform, Target);

            if (isVisible)
            {
                Vector3 futurePos = GameplayUtils.PredictPosition(Target, TargetRigidbody, 0.5f);
                enemyRotation.UpdateRotation(futurePos);
                enemyAnimations?.UpdateAnimations(navMeshAgent);
            
                if (!enemyAttacker.OnCooldown)
                {
                    enemyMovement?.UpdateMovement(navMeshAgent, navMeshAgent.transform);
                    enemyAttacker.Attack();
                }
            }
            else
            {
                enemyRotation.UpdateRotation(Target);
                enemyMovement?.UpdateMovement(navMeshAgent, Target);
            }
        }
    }
}
