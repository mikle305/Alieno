using System;
using System.Collections.Generic;
using GamePlay.UnitsComponents;
using UnityEngine;

namespace Services
{
    public class EnemiesDeathObserver
    {
        private readonly ObjectsProvider _objectsProvider;
        
        private int _aliveEnemiesCount;

        public event Action AllCleared;


        public EnemiesDeathObserver(ObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
        }

        public void Init()
        {
            List<Transform> aliveEnemies = _objectsProvider.AliveEnemies;
            _aliveEnemiesCount = aliveEnemies.Count;
            foreach (Transform enemy in aliveEnemies)
            {
                var enemyDeath = enemy.GetComponent<Death>();
                enemyDeath.Happened += () => OnEnemyDied(enemy);
            }
        }

        private void OnEnemyDied(Transform enemy)
        {
            _objectsProvider.AliveEnemies.Remove(enemy);
            _aliveEnemiesCount--;
            if (_aliveEnemiesCount == 0)
                AllCleared?.Invoke();
        }
    }
}