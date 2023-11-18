using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class MultishotLevelData : AbilityLevelData
    {
        [field: SerializeField] public int AdditionsCount { get; private set; }
        [field: SerializeField] public float ShotDelay { get; private set; }
    }
}