using System;
using GamePlay.Abilities;
using UnityEngine;

namespace GamePlay.Characteristics
{
    [Serializable]
    public class ProjectileSpawnData
    {
        [field: SerializeField] public AbilityId AbilityId { get; private set; }
        [field: SerializeField] public SpawnByLevel[] Levels { get; private set; } = { };
    }
}