using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy Ai/Soldier AI")]
public class BasicSoldierAI : EnemyAI
{
    public override void Execute(NavMeshAgent _navMeshAgent, EnemyMovement _enemyMovement, EnemyRotation _enemyRotation,
        EnemyAnimations _enemyAnimations,EnemyAttacker _enemyAttacker)
    {
        _enemyRotation?.UpdateRotation(Target);
        _enemyAnimations?.UpdateAnimations(_navMeshAgent);
        bool isVisible = GameplayUtils.IsVisible(_enemyRotation.transform, Target);
        if (!_enemyAttacker.OnCooldown && isVisible)
        {
            _enemyMovement?.UpdateMovement(_navMeshAgent, _navMeshAgent.transform);

            _enemyAttacker?.Attack();
        }
        else if (!isVisible)
        {
            _enemyMovement?.UpdateMovement(_navMeshAgent, Target);

        }
    }
}
