using System.Collections.Generic;
using System.Linq;
using GamePlay.Other.Ids;
using UnityEngine;

namespace StaticData.Enemies
{
    [CreateAssetMenu(menuName = "StaticData/Enemies Config", fileName = "EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyEntry[] Enemies { get; private set; } = { };


        private Dictionary<EnemyId, EnemyEntry> _enemiesMap;
            
        
        public EnemyEntry GetEnemy(EnemyId id)
            => (_enemiesMap ??= CreateEnemiesMap())[id];

        private Dictionary<EnemyId, EnemyEntry> CreateEnemiesMap()
            => Enemies.ToDictionary(x => x.Id, x => x);
    }
}