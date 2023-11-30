using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class FlameLevelData : AbilityLevelData
    {
        [field: SerializeField, Min(0)] public float DamageCoefficient { get; private set; }
        [field: SerializeField, Min(0)] public float Rate { get; private set; } = 1;
        [field: SerializeField, Min(1)] public int Count { get; private set; } = 1;
    }
}