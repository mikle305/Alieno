using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class DashBoostLevelData : AbilityLevelData
    {
        [field: SerializeField, Range(0, 1)] public float RateCoefficient { get; private set; } = 1;
        [field: SerializeField, Min(1)] public float MoveSpeedCoefficient { get; private set; } = 1;
    }
}