using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class ForwardShotLevelData : AbilityLevelData
    {
        [field: SerializeField] public int ShotsCount { get; private set; }
    }
}