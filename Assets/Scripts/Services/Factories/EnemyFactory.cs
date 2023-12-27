using GamePlay.Enemy;
using GamePlay.Other.Ids;
using UnityEngine;

namespace Services.Factories
{
    public class EnemyFactory
    {
        private readonly StaticDataService _staticDataService;
        private readonly ObjectActivator _objectActivator;


        public EnemyFactory(StaticDataService staticDataService, ObjectActivator objectActivator)
        {
            _objectActivator = objectActivator;
            _staticDataService = staticDataService;
        }

        public GameObject Create(EnemySpawn enemySpawn) 
            => SpawnNew(enemySpawn);

        private GameObject SpawnNew(EnemySpawn enemySpawn)
        {
            GameObject prefab = GetPrefab(enemySpawn.Id);
            Transform spawnTransform = enemySpawn.transform;
            return _objectActivator.Instantiate(prefab, spawnTransform.position, Quaternion.identity, spawnTransform);
        }

        private GameObject GetPrefab(EnemyId id)
            => _staticDataService
                .GetPrefabsConfig()
                .GetEnemy(id);
    }
}