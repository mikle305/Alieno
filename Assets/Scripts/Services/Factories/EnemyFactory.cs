using System.Collections.Generic;
using System.Linq;
using Additional.Game;
using Additional.Utils;
using GamePlay.Enemy;
using UnityEngine;

namespace Services
{
    public class EnemyFactory : MonoSingleton<EnemyFactory>
    {
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
            if (!_idsMap.TryGetValue(enemySpawn.Id, out PoolId poolId))
                ThrowUtils.NeedToCreatePool();
                
            return _objectsFactory.Create(poolId, enemySpawn.transform.position);
        }

        private void InitPoolIdsMap()
        {
            _idsMap = _staticDataService
                .GetPoolIdsConfig()
                .EnemyEntries
                .ToDictionary(e => e.EnemyId, e => e.PoolId);
        }
    }

}