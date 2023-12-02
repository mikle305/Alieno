using Additional.Game;
using GamePlay.Enemy;
using GamePlay.Other.Ids;
using UnityEngine;

namespace Services.Factories
{
    public class EnemyFactory : MonoSingleton<EnemyFactory>
    {
        private StaticDataService _staticDataService;


        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
        }

        public GameObject Create(EnemySpawn enemySpawn) 
            => SpawnNew(enemySpawn);

        private GameObject SpawnNew(EnemySpawn enemySpawn)
        {
            GameObject prefab = GetPrefab(enemySpawn.Id);
            Transform spawnTransform = enemySpawn.transform;
            return Instantiate(prefab, spawnTransform.position, Quaternion.identity, spawnTransform);
        }

        private GameObject GetPrefab(EnemyId id)
            => _staticDataService
                .GetPrefabsConfig()
                .GetEnemy(id);
    }
}