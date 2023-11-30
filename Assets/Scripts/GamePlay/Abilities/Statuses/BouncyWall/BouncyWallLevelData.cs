using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class BouncyWallLevelData : AbilityLevelData
    {
        [field: SerializeField] public int Count { get; private set; }
    }
}