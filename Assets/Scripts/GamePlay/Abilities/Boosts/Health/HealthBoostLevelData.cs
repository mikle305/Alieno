using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class HealthBoostLevelData : AbilityLevelData
    {
        [field: SerializeField, Min(1)] public float HealthCoefficient { get; private set; } = 1;
    }
}