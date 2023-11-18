using System;
using StaticData.Abilities;

namespace GamePlay.Abilities
{
    [Serializable]
    public class ForwardShotData : AbilityData<ForwardShotLevelData>
    {
        public override AbilityId Id => AbilityId.ForwardShot;
        public override Type ComponentType { get; } = typeof(ForwardShotComponent);
    }
}