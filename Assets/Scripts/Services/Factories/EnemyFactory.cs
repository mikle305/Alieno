using GamePlay.Characteristics;
using GamePlay.Enemy;
using GamePlay.Other.Ids;
using StaticData.Enemies;
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

        public GameObject Create(EnemySpawn enemySpawn, float roomHealthMultiplier)
        {
            GameObject enemy = SpawnEnemy(enemySpawn);
            InitHealth(enemy, enemySpawn, roomHealthMultiplier);
            return enemy;
        }

        private GameObject SpawnEnemy(EnemySpawn enemySpawn)
        {
            GameObject prefab = GetPrefab(enemySpawn.Id);
            Transform spawnTransform = enemySpawn.transform;
            GameObject enemy = _objectActivator.Instantiate(prefab, spawnTransform.position, Quaternion.identity, spawnTransform);
            return enemy;
        }

        private void InitHealth(GameObject enemy, EnemySpawn enemySpawn, float roomHealthMultiplier)
        {
            EnemyEntry enemyConfig = _staticDataService.GetEnemiesConfig().GetEnemy(enemySpawn.Id);
            float health = enemyConfig.Health * roomHealthMultiplier * enemySpawn.HealthMultiplier;
            enemy.GetComponent<HealthData>().Init(health, health);
        }

        private GameObject GetPrefab(EnemyId id)
            => _staticDataService
                .GetPrefabsConfig()
                .GetEnemy(id);
    }
}