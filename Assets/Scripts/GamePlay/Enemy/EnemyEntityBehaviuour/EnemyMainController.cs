using Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyMainController : MonoBehaviour
{
    [FormerlySerializedAs("_ai")] [SerializeField] private EnemyAI _aiPrefab;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyRotation _enemyRotation;
    [SerializeField] private EnemyAnimations _enemyAnimations;
    [SerializeField] private EnemyAttacker _enemyAttacker;

    private ObjectsProvider _objectsProvider;
    private EnemyAI _ai;
    
    private void Start()
    {
        _ai = Instantiate(_aiPrefab);
        _objectsProvider = ObjectsProvider.Instance;
    }

    private void Update()
    {
        _ai.Target = _objectsProvider.Character.transform;
        _ai.Execute(_navMeshAgent,_enemyMovement,_enemyRotation,_enemyAnimations,_enemyAttacker);
    }

}
