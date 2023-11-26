using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class HealthBoostLevelData : AbilityLevelData
    {
        [field: SerializeField, Min(0)] public int PercentsBuff { get; private set; }
    }
}