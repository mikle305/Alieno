using Services;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMainController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyRotation _enemyRotation;
    [SerializeField] private EnemyAnimations _enemyAnimations;

    private ObjectsProvider _objectsProvider;

    
    private void Start()
    {
        _objectsProvider = ObjectsProvider.Instance;
    }

    private void Update()
    {
        Transform target = _objectsProvider.Character.transform;
        print(target);
        _enemyMovement?.UpdateMovement(_navMeshAgent, target);
        _enemyRotation?.UpdateRotation(target);
        _enemyAnimations?.UpdateAnimations(_navMeshAgent);
    }

}
