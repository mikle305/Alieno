using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class DashBoostData : AbilityData<DashBoostLevelData>
    {
        public override AbilityId Id => AbilityId.DashBoost;
        public override Type ComponentType => typeof(DashBoostComponent);
    }
}