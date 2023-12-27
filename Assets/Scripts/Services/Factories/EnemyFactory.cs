using GamePlay.Enemy;
using GamePlay.Other.Ids;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Services.Factories
{
    public class EnemyFactory
    {
        private readonly StaticDataService _staticDataService;
        private readonly IObjectResolver _monoResolver;


        public EnemyFactory(StaticDataService staticDataService, IObjectResolver monoResolver)
        {
            _monoResolver = monoResolver;
            _staticDataService = staticDataService;
        }

        public GameObject Create(EnemySpawn enemySpawn) 
            => SpawnNew(enemySpawn);

        private GameObject SpawnNew(EnemySpawn enemySpawn)
        {
            GameObject prefab = GetPrefab(enemySpawn.Id);
            Transform spawnTransform = enemySpawn.transform;
            return _monoResolver.Instantiate(prefab, spawnTransform.position, Quaternion.identity, spawnTransform);
        }

        private GameObject GetPrefab(EnemyId id)
            => _staticDataService
                .GetPrefabsConfig()
                .GetEnemy(id);
    }
}