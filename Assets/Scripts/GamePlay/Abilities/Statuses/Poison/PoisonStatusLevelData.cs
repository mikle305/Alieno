using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class PoisonStatusLevelData : AbilityLevelData
    {
        [field: SerializeField] public float DamagePercents { get; private set; }
        [field: SerializeField] public float Rate { get; private set; }
    }
}