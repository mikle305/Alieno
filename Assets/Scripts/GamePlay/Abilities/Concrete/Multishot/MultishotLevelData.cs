using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class MultishotLevelData : AbilityLevelData
    {
        [field: SerializeField, Min(1)] public int AdditionsCount { get; private set; }
        [field: SerializeField, Min(0)] public float ShotDelay { get; private set; }


        public HashSet<AbilityId> ApplicableAbilities { get; private set; } = new()
        {
            AbilityId.ForwardShot,
            AbilityId.DiagonalShot,
            AbilityId.BackShot,
            AbilityId.SideShot,
        };
    }
}