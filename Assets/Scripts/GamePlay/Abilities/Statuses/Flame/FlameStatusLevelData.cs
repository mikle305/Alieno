using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class FlameStatusLevelData : AbilityLevelData
    {
        [field: SerializeField] public float DamagePercents { get; private set; }
        [field: SerializeField] public float Rate { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}