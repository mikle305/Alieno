using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class MultishotData : AbilityData<MultishotLevelData>
    {
        public override AbilityId Id => AbilityId.Multishot;
        public override Type ComponentType { get; } = typeof(MultishotComponent);
    }
}