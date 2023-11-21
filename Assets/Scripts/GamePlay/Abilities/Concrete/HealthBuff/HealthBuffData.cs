using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class HealthBuffData : AbilityData<HealthBuffLevelData>
    {
        public override AbilityId Id => AbilityId.HealthBuff;
        public override Type ComponentType { get; } = typeof(HealthBuffComponent);
    }
}