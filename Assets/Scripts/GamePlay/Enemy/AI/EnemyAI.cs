using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    public abstract class EnemyAI : ScriptableObject
    {
        public Transform Target { get; set; }
        public Rigidbody TargetRigidbody { get; set; }
        
        public abstract void Execute(
            NavMeshAgent navMeshAgent, 
            EnemyMovement enemyMovement,
            EnemyRotation enemyRotation, 
            EnemyAnimations enemyAnimations, 
            EnemyAttacker enemyAttacker);
    }
}