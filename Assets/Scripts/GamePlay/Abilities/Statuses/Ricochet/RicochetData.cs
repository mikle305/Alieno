using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class RicochetData : AbilityData<RicochetLevelData>
    {
        public override AbilityId Id => AbilityId.Ricochet;
        public override Type ComponentType => typeof(StatusAbilityComponent<RicochetData, RicochetLevelData>);
    }
}