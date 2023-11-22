using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Abilities;
using GamePlay.StatsSystem;
using Services;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class ProjectileAttackData : MonoBehaviour
    {
        [field: SerializeField] public ObjectId ObjectId { get; private set; }
        [SerializeField] private float _defaultUseRate;
        [SerializeField] private float _defaultDamage;
        [SerializeField] private float _defaultMoveSpeed;
        [SerializeField] private ProjectileSpawnData[] _spawnsData = {};

        public ModifiableStat Damage { get; private set; }
        public ModifiableStat MoveSpeed { get; private set; }
        public ModifiableStat UseRate { get; private set; }

        private Dictionary<AbilityId, SpawnByLevel[]> _spawnsMap;

        
        private void Awake()
        {
            _spawnsMap = _spawnsData.ToDictionary(spawnData => spawnData.AbilityId, spawnData => spawnData.Levels);
            UseRate = new ModifiableStat(_defaultUseRate);
            Damage = new ModifiableStat(_defaultDamage);
            MoveSpeed = new ModifiableStat(_defaultMoveSpeed);
        }

        public Vector3[] GetSpawnPoints(AbilityId abilityId, int levelId)
        {
            if (levelId < 1)
                throw new InvalidOperationException("Ability LevelId can't be less than 1");
            
            if (!_spawnsMap.TryGetValue(abilityId, out SpawnByLevel[] levels))
                throw new InvalidOperationException($"SpawnsData not filled for {abilityId.ToString()}");

            if (levels.Length < levelId)
                throw new InvalidOperationException(
                    $"Level with index {levelId - 1} not filled in SpawnsData for {abilityId.ToString()}");
                
            return levels[levelId - 1].Spawns;
        }
    }
}