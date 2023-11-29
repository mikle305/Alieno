using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy Ai/Grunt AI")]
public class GruntSoldierAI : EnemyAI
{
    public override void Execute(NavMeshAgent _navMeshAgent, EnemyMovement _enemyMovement, EnemyRotation _enemyRotation,
        EnemyAnimations _enemyAnimations,EnemyAttacker _enemyAttacker)
    {
        bool isVisible = GameplayUtils.IsVisible(_enemyRotation.transform, Target);

        if (isVisible)
        {
            Vector3 futurePos = GameplayUtils.CalcFuturePos(Target, TargetRigidbody, 0.5f);
            _enemyRotation?.UpdateRotation(futurePos);
            _enemyAnimations?.UpdateAnimations(_navMeshAgent);
            
            if (!_enemyAttacker.OnCooldown)
            {
                _enemyAttacker?.Attack();
            }
        }
        else
        {
            _enemyRotation?.UpdateRotation(Target);
            _enemyMovement?.UpdateMovement(_navMeshAgent, Target);
        }
    }
}
