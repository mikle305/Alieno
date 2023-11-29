using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class AttackDmgBoostLevelData : AbilityLevelData
    {
        [field: SerializeField, Min(1)] public float MultiplierCoefficient { get; private set; } = 1;
    }
}