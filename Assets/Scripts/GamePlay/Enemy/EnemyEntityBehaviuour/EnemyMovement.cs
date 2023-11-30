using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public void UpdateMovement(NavMeshAgent _agent, Transform _target)
        {
            if (_target != null)
                _agent.destination = _target.position;
        }
    
        public void UpdateMovement(NavMeshAgent _agent, Vector3 _target)
        {
            if (_target != null)
                _agent.destination = _target;
        }
    }
}
