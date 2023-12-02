using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    public abstract class EnemyAI : ScriptableObject
    {
        public Transform Target { get; set; }
        public Rigidbody TargetRigidbody { get; set; }
        public abstract void Execute(NavMeshAgent _navMeshAgent,EnemyMovement _enemyMovement,EnemyRotation _enemyRotation,EnemyAnimations _enemyAnimations,EnemyAttacker enemyAttacker);
    }
}
