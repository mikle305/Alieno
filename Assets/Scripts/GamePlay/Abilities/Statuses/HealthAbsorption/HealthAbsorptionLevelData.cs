using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class HealthAbsorptionLevelData : AbilityLevelData
    {
        [field: SerializeField] public float MaxHealthCoefficient { get; private set; }
    }
}