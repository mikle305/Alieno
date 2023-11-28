using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy Ai/Soldier AI")]
public class BasicSoldierAI : EnemyAI
{
    public override void Execute(NavMeshAgent _navMeshAgent, EnemyMovement _enemyMovement, EnemyRotation _enemyRotation,
        EnemyAnimations _enemyAnimations,EnemyAttacker _enemyAttacker)
    {
        _enemyMovement?.UpdateMovement(_navMeshAgent, Target);
        _enemyRotation?.UpdateRotation(Target);
        _enemyAnimations?.UpdateAnimations(_navMeshAgent);
        if(!_enemyAttacker.OnCooldown)
            _enemyAttacker?.Attack();
    }
}
