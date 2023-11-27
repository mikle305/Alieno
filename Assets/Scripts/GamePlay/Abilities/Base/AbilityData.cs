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
        public abstract int MaxLevel { get; }
    }

    [Serializable]
    public abstract class AbilityData<TLevelData> : AbilityData
        where TLevelData : AbilityLevelData, new()
    {
        [field: SerializeField] public TLevelData[] Levels { get; private set; } = { new() };
        public sealed override int MaxLevel => Levels.Length;
    }
}