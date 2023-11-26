using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class PoisonStatusData : AbilityData<PoisonStatusLevelData>
    {
        public override AbilityId Id => AbilityId.Poison;
        public override Type ComponentType => typeof(StatusAbilityComponent<PoisonStatusData, PoisonStatusLevelData>);
    }
}