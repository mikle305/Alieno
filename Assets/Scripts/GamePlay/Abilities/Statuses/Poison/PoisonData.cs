using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class PoisonData : AbilityData<PoisonLevelData>
    {
        public override AbilityId Id => AbilityId.Poison;
        public override Type ComponentType => typeof(StatusAbilityComponent<PoisonData, PoisonLevelData>);
    }
}