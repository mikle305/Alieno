using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class DiagonalShotData : AbilityData<ShotLevelData>
    {
        public override AbilityId Id => AbilityId.DiagonalShot;
        public override Type ComponentType { get; } = typeof(DiagonalShotComponent);
    }
}