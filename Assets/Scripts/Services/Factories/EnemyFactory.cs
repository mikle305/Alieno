using System;
using System.Collections.Generic;
using System.Linq;
using Additional.Game;
using GamePlay.Enemy;
using UnityEngine;

namespace Services
{
    public class EnemyFactory : MonoSingleton<EnemyFactory>
    {
        [SerializeField] private EnemyEntry[] _enemyPrefabs = {};
        
        private StaticDataService _staticDataService;
        private ObjectsFactory _objectsFactory;
        private Dictionary<EnemyId, PoolId> _idsMap;


        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
            _objectsFactory = ObjectsFactory.Instance;
            InitPoolIdsMap();
        }

        public GameObject Create(EnemySpawn enemySpawn)
        {
            GameObject prefab = _enemyPrefabs.First(x => x.Id == enemySpawn.Id).Prefab;
            return Instantiate(prefab, enemySpawn.transform.position, Quaternion.identity, enemySpawn.transform);
        }

        private void InitPoolIdsMap()
        {
            _idsMap = _staticDataService
                .GetPoolIdsConfig()
                .EnemyEntries
                .ToDictionary(e => e.EnemyId, e => e.PoolId);
        }
    }

    [Serializable]
    public class EnemyEntry
    {
        [field: SerializeField] public EnemyId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}