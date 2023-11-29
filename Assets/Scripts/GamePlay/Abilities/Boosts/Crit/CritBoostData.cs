using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class CritBoostData : AbilityData<CritBoostLevelData>
    {
        public override AbilityId Id => AbilityId.CritBoost;
        public override Type ComponentType => typeof(CritBoostComponent);
    }
}