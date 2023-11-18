using System;
using StaticData.Abilities;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class RicochetLevelData : AbilityLevelData
    {
        [field: SerializeField] public int RicochetsCount { get; private set; }
    }
}