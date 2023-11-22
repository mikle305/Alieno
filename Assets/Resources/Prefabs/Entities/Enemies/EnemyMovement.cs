using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public void UpdateMovement(NavMeshAgent _agent, Transform _target)
    {
        if (_target != null)
            _agent.destination = _target.position;
    }
}
