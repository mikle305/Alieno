using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class AttackSpdBoostData : AbilityData<AttackSpdBoostLevelData>
    {
        public override AbilityId Id => AbilityId.AttackSpeedBoost;
        public override Type ComponentType => typeof(AttackSpdBoostComponent);
    }
}