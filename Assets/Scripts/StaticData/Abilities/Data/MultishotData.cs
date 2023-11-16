using System;
using GamePlay.Abilities;
using UnityEngine;

namespace StaticData.Abilities
{
    [Serializable]
    public class MultishotData : AbilityData
    {
        [field: SerializeField] public int PlusCount { get; private set; } = 1;

        public override AbilityId Id => AbilityId.MultishotProjectile;
        public override Type ComponentType { get; } = typeof(MultishotComponent);
    }
}