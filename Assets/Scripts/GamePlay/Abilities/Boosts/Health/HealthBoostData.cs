using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class HealthBoostData : AbilityData<HealthBoostLevelData>
    {
        public override AbilityId Id => AbilityId.HealthBoost;
        public override Type ComponentType => typeof(HealthBoostComponent);
    }
}