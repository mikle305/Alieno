using System;
using GamePlay.Abilities;
using TriInspector;
using UnityEngine;

namespace StaticData.Abilities
{
    [Serializable]
    public abstract class AbilityData
    {
        [field: SerializeField] public string Name { get; private set; }
        [ShowInInspector, PropertyOrder(0)] public abstract AbilityId Id { get; }

        public abstract Type ComponentType { get; }
    }
}