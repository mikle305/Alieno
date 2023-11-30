using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class FlameData : AbilityData<FlameLevelData>
    {
        public override AbilityId Id => AbilityId.Flame;
        public override Type ComponentType => typeof(StatusAbilityComponent<FlameData, FlameLevelData>);
    }
}