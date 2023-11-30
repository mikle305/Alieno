using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Ai/Turret AI")]
    public class TurretAI : EnemyAI
    {
        public override void Execute(NavMeshAgent _navMeshAgent, EnemyMovement _enemyMovement, EnemyRotation _enemyRotation,
            EnemyAnimations _enemyAnimations,EnemyAttacker _enemyAttacker)
        {
            _enemyRotation?.UpdateRotation(Target);
            if (!_enemyAttacker.OnCooldown && GameplayUtils.IsVisible(_enemyRotation.transform, Target))
                _enemyAttacker?.Attack();
        }
    }
}