using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class SideShotData : AbilityData<ShotLevelData>
    {
        public override AbilityId Id => AbilityId.SideShot;
        public override Type ComponentType => typeof(ShotComponent<SideShotData>);
    }
}