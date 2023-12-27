using Additional.Constants;
using Additional.Utils;
using GamePlay.Characteristics;
using Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using VContainer;

namespace GamePlay.Enemy
{
    public class EnemyMainController : MonoBehaviour
    {
        [FormerlySerializedAs("_ai")] 
        [SerializeField] private EnemyAI _aiPrefab;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyRotation _enemyRotation;
        [SerializeField] private EnemyAnimations _enemyAnimations;
        [SerializeField] private EnemyAttacker _enemyAttacker;
        [SerializeField] private HealthData _healthData;

        private readonly Collider[] _nearbyEnemies = new Collider[25];
        private ObjectsProvider _objectsProvider;
        private EnemyAI _ai;
        private bool _isAwake;


        [Inject]
        public void Construct(ObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
        }

        private void Start()
        {
            _ai = Instantiate(_aiPrefab);
            _ai.Target = _objectsProvider.Character.transform;
            _ai.TargetRigidbody = _objectsProvider.CharacterRigidbody;
            _healthData.ValueChanged += AwakeEnemy;
        }

        private void AwakeEnemy()
        {
            if (_isAwake)
                return;

            _isAwake = true;
            _healthData.ValueChanged -= AwakeEnemy;
            AwakeNearby();
        }

        private void AwakeNearby()
        {
            int nearbyCount = Physics.OverlapSphereNonAlloc(transform.position, 8f, _nearbyEnemies, GameConstants.EnemyLayer);

            for (var i = 0; i < nearbyCount; i++)
            {
                _nearbyEnemies[i].TryGetComponent(out EnemyMainController controller);
                controller?.AwakeEnemy();
            }
        }

        private void Update()
        {
            if (_objectsProvider.Character == null)
                return;

            if (!_isAwake)
            {
                if (GameplayUtils.IsVisible(transform, _objectsProvider.Character.transform))
                {
                    AwakeEnemy();
                }

                return;
            }

            _ai.Execute(_navMeshAgent, _enemyMovement, _enemyRotation, _enemyAnimations, _enemyAttacker);
        }
    }
}