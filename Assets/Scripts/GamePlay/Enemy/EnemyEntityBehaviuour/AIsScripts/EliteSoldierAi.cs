using Additional.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Ai/EliteSoldier AI")]
    public class EliteSoldierAI : EnemyAI
    {
        [SerializeField] private float _delayBeforeAttack;

        [SerializeField] private Vector3 _attackIndicatorOffset = new Vector3(0, 0.2f, 0);

        private Vector3 _shootingPos;
        public override void Execute(NavMeshAgent _navMeshAgent, EnemyMovement _enemyMovement, EnemyRotation _enemyRotation,
            EnemyAnimations _enemyAnimations,EnemyAttacker _enemyAttacker)
        {

            _enemyAnimations?.UpdateAnimations(_navMeshAgent);
            
            if (!_enemyAttacker.OnCooldown)
            {
                _shootingPos = GameplayUtils.CalcFuturePos(Target, TargetRigidbody, _delayBeforeAttack);
                _enemyAnimations?.SpawnAttackIndicatorTowards(_navMeshAgent.transform.position+_attackIndicatorOffset,_delayBeforeAttack*1.4f,_shootingPos);
                _enemyRotation?.UpdateRotation(_shootingPos);
                _enemyAttacker?.AttackWithDelay(_delayBeforeAttack);
            }
            else
            {
                _enemyRotation?.UpdateRotation(_shootingPos);   
            }
        }
    }
}

