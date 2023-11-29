using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class ObstaclePenetrationLevelData : AbilityLevelData
    {
        /// <summary>
        /// Endless when -1
        /// </summary>
        [field: SerializeField] public int Count { get; private set; } = -1;
    }
}