using System;

namespace GamePlay.Abilities
{
    [Serializable]
    public class ObstaclePenetrationData : AbilityData<ObstaclePenetrationLevelData>
    {
        public override AbilityId Id => AbilityId.ObstaclePenetration;
        public override Type ComponentType => typeof(StatusAbilityComponent<ObstaclePenetrationData, ObstaclePenetrationLevelData>);
    }
}