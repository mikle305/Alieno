using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class BouncyWallData : AbilityData<BouncyWallLevelData>
    {
        public override AbilityId Id => AbilityId.BouncyWall;
        public override Type ComponentType => typeof(StatusAbilityComponent<BouncyWallData, BouncyWallLevelData>);
    }
}