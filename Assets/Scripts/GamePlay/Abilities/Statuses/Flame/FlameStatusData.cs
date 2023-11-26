using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class FlameStatusData : AbilityData<FlameStatusLevelData>
    {
        public override AbilityId Id => AbilityId.Flame;
        public override Type ComponentType => typeof(StatusAbilityComponent<FlameStatusData, FlameStatusLevelData>);
    }
}