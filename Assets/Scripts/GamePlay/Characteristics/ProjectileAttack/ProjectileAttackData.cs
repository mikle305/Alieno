using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Abilities;
using GamePlay.Other.Ids;
using GamePlay.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Characteristics
{
    public class ProjectileAttackData : MonoBehaviour
    {
        [FormerlySerializedAs("_defaultUseRate")] [SerializeField] private float _defaultAttackRate;
        [FormerlySerializedAs("_defaultDamage")] [SerializeField] private float _defaultAttackDamage;
        [SerializeField] private float _defaultMoveSpeed;
        [SerializeField] private ProjectileSpawnData[] _spawnsData = {};
        [field: SerializeField] public ProjectileId ProjectileId { get; private set; }


        public ModifiableStat AttackRate { get; private set; }
        public ModifiableStat AttackDamage { get; private set; }
        public ModifiableStat CritChance { get; private set; }
        public ModifiableStat CritMultiplier { get; private set; }
        public ModifiableStat MoveSpeed { get; private set; }


        private Dictionary<AbilityId, SpawnByLevel[]> _spawnsMap;

        
        private void Awake()
        {
            _spawnsMap = _spawnsData.ToDictionary(spawnData => spawnData.AbilityId, spawnData => spawnData.Levels);
            AttackRate = new ModifiableStat(_defaultAttackRate);
            AttackDamage = new ModifiableStat(_defaultAttackDamage);
            CritChance = new ModifiableStat();
            CritMultiplier = new ModifiableStat();
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