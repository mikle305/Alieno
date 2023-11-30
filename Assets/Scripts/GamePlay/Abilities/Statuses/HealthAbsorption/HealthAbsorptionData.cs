using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class HealthAbsorptionData : AbilityData<HealthAbsorptionLevelData>
    {
        public override AbilityId Id => AbilityId.HealthAbsorption;
        public override Type ComponentType => typeof(StatusAbilityComponent<HealthAbsorptionData, HealthAbsorptionLevelData>);
    }
}