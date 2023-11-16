using System;
using GamePlay.Abilities;
using UnityEngine;

namespace StaticData.Abilities
{
    [Serializable]
    public class RicochetData : AbilityData
    {
        [field: SerializeField] public int RicochetsCount { get; private set; } = 2;

        public override AbilityId Id => AbilityId.RicochetProjectile;
        public override Type ComponentType { get; } = typeof(RicochetComponent);
    }
}