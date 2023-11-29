using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class AttackDmgBoostData : AbilityData<AttackDmgBoostLevelData>
    {
        public override AbilityId Id => AbilityId.AttackDamageBoost;
        public override Type ComponentType => typeof(AttackDmgBoostComponent);
    }
}