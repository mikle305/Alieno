using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class VampirismLevelData : AbilityLevelData
    {
        [field: SerializeField, Min(0)] public float DamageCoefficient { get; private set; }
    }
}