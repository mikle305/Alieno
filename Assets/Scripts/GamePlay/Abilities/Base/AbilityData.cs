using System;
using TriInspector;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public abstract class AbilityData
    {
        [ShowInInspector, PropertyOrder(0)] public abstract AbilityId Id { get; }

        public abstract Type ComponentType { get; }
    }

    [Serializable]
    public abstract class AbilityData<TLevelData> : AbilityData
        where TLevelData : AbilityLevelData, new()
    {
        [field: SerializeField] public TLevelData[] Levels { get; private set; } = { new() };
    }
}