using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class CritBoostLevelData : AbilityLevelData
    {
        [field: SerializeField, Range(0, 1)] public float ChanceCoefficient { get; private set; }
        [field: SerializeField, Min(1)] public float MultiplierCoefficient { get; private set; } = 1;
    }
}