using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class RicochetLevelData : AbilityLevelData
    {
        [field: SerializeField, Min(1)] public int RicochetsCount { get; private set; }
    }
}