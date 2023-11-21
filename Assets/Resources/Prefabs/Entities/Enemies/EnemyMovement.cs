using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;
    void Awake()
    {
        _agent.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_target != null)
            _agent.destination = _target.position;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
