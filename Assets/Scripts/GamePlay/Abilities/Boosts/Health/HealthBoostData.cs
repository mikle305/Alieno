using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class HealthBoostData : AbilityData<HealthBoostLevelData>
    {
        public override AbilityId Id => AbilityId.HealthBuff;
        public override Type ComponentType => typeof(HealthBuffComponent);
    }
}