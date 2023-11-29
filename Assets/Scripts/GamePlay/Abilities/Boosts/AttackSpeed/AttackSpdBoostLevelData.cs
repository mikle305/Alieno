using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class AttackSpdBoostLevelData : AbilityLevelData
    {
        [field: SerializeField, Range(0, 1)] public float RateCoefficient { get; private set; } = 1;
    }
}