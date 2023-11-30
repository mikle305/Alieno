using System;
using Additional.Utils;
using GamePlay.Characteristics;
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
    [SerializeField] private HealthData _healthData;

    private ObjectsProvider _objectsProvider;
    private EnemyAI _ai;
    private bool _isAwake;
    private void Start()
    {
        _ai = Instantiate(_aiPrefab);
        _objectsProvider = ObjectsProvider.Instance;
        _ai.Target = _objectsProvider.Character.transform;
        _ai.TargetRigidbody = _objectsProvider.CharacterRigidbody;
        _healthData.ValueChanged += AwakeEnemy;
    }

    public void AwakeEnemy()
    {
        if(_isAwake)
            return;
        
        _isAwake = true;
        _healthData.ValueChanged -= AwakeEnemy;
        AwakeNearby();
    }

    public void AwakeNearby()
    {
        var enemyLayer = GameplayUtils.GetEnemyLayerMask();
        var colliders = Physics.OverlapSphere(transform.position, 8f, enemyLayer);
        
        foreach(var collider in colliders) {
            collider.TryGetComponent<EnemyMainController>(out EnemyMainController controller);
            controller?.AwakeEnemy();
        }
    }
    
    private void Update()
    {
        if (!_isAwake)
        {
            if (GameplayUtils.IsVisible(transform, _objectsProvider.Character.transform))
            {
                AwakeEnemy();
            }
            
            return;
        }

        _ai.Execute(_navMeshAgent,_enemyMovement,_enemyRotation,_enemyAnimations,_enemyAttacker);
    }

}
