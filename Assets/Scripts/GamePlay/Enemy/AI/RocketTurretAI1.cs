using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Ai/RocketTurret AI")]
    public class RocketTurretAI : EnemyAI
    {
        public override void Execute(
            NavMeshAgent navMeshAgent, 
            EnemyMovement enemyMovement,
            EnemyRotation enemyRotation,
            EnemyAnimations enemyAnimations, 
            EnemyAttacker enemyAttacker)
        {
            enemyRotation?.UpdateRotation(Target);
            if (!enemyAttacker.OnCooldown && GameplayUtils.IsVisible(enemyRotation?.transform, Target))
                enemyAttacker.Attack();
        }
    }
}