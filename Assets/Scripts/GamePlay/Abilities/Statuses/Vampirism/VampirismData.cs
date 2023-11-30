using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class VampirismData : AbilityData<VampirismLevelData> 
    {
        public override AbilityId Id => AbilityId.Vampirism;

        public override Type ComponentType => typeof(StatusAbilityComponent<VampirismData, VampirismLevelData>);
    }
}