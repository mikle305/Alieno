using System.Collections.Generic;
using System.Linq;
using GamePlay.Abilities;
using GamePlay.StatsSystem;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class ProjectileAttack : MonoBehaviour
    {
        [SerializeField] private float _defaultUseRate;
        [SerializeField] private float _defaultDamage;
        [SerializeField] private float _defaultMoveSpeed;
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [SerializeField] private ProjectileSpawnData[] _spawnsData = {};
        
        private Dictionary<AbilityId, SpawnByLevel[]> _spawnsMap;

        public ModifiableStat Damage { get; private set; }
        public ModifiableStat MoveSpeed { get; private set; }
        public ModifiableStat UseRate { get; private set; }
        

        private void Awake()
        {
            _spawnsMap = _spawnsData.ToDictionary(spawnData => spawnData.AbilityId, spawnData => spawnData.Levels);
            UseRate = new ModifiableStat(_defaultUseRate);
            Damage = new ModifiableStat(_defaultDamage);
            MoveSpeed = new ModifiableStat(_defaultMoveSpeed);
        }

        public Vector3[] GetSpawnPoints(AbilityId abilityId, int levelId)
            => _spawnsMap[abilityId][levelId - 1].Spawns;
    }
}