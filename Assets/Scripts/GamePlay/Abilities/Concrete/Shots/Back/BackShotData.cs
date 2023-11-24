using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class BackShotData : AbilityData<ShotLevelData>
    {
        public override AbilityId Id => AbilityId.BackShot;
        public override Type ComponentType => typeof(BackShotComponent);
    }
}