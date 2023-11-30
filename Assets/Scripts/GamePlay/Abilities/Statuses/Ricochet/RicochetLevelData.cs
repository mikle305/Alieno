using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class RicochetLevelData : AbilityLevelData
    {
        [field: SerializeField] public int Count { get; private set; }
    }
}