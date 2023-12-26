using System;
using System.Collections.Generic;
using GamePlay.Abilities;
using GamePlay.Statuses;

namespace Services.Statuses
{
    public class StatusesMapper
    {
        private readonly Dictionary<Type, Func<AbilityLevelData, Status>> _factoriesMap = new()
        {
            { typeof(FlameLevelData), CreateFlameStatus },
            { typeof(PoisonLevelData), CreatePoisonStatus },
            { typeof(ObstaclePenetrationLevelData), CreateObstaclePenetrationStatus },
            { typeof(VampirismLevelData), CreateVampirismStatus },
            { typeof(HealthAbsorptionLevelData), CreateHealthAbsorptionStatus },
            { typeof(RicochetLevelData), CreateRicochetStatus },
            { typeof(BouncyWallLevelData), CreateBouncyWallStatus },
        };

        private readonly Dictionary<Type, Type> _typesMap = new()
        {
            { typeof(FlameLevelData), typeof(FlameStatus) },
            { typeof(PoisonLevelData), typeof(PoisonStatus) },
            { typeof(ObstaclePenetrationLevelData), typeof(ObstaclePenetrationStatus) },
            { typeof(VampirismLevelData), typeof(VampirismStatus) },
            { typeof(HealthAbsorptionLevelData), typeof(HealthAbsorptionStatus) },
            { typeof(RicochetLevelData), typeof(RicochetStatus) },
            { typeof(BouncyWallLevelData), typeof(BouncyWallStatus) },
        };

        public Status Map(AbilityLevelData abilityLevel, out Type statusType)
        {
            Type abilityType = abilityLevel.GetType();
            statusType = _typesMap[abilityType];
            return _factoriesMap[abilityType].Invoke(abilityLevel);
        }

        private static Status CreateFlameStatus(AbilityLevelData ability)
        {
            var flameAbility = (FlameLevelData)ability;
            return new FlameStatus
            {
                DamageCoefficient = flameAbility.DamageCoefficient,
                Rate = flameAbility.Rate,
                CountLeft = flameAbility.Count,
            };
        }

        private static Status CreatePoisonStatus(AbilityLevelData ability)
        {
            var poisonAbility = (PoisonLevelData)ability;
            return new PoisonStatus
            {
                DamageCoefficient = poisonAbility.DamageCoefficient,
                Rate = poisonAbility.Rate,
                CountLeft = poisonAbility.Count,
            };
        }

        private static Status CreateObstaclePenetrationStatus(AbilityLevelData ability)
        {
            var obstaclePenetrationAbility = (ObstaclePenetrationLevelData)ability;
            return new ObstaclePenetrationStatus
            {
                CountLeft = obstaclePenetrationAbility.Count,
            };
        }

        private static Status CreateHealthAbsorptionStatus(AbilityLevelData ability)
        {
            var healthAbsorptionAbility = (HealthAbsorptionLevelData)ability;
            return new HealthAbsorptionStatus
            {
                MaxHealthCoefficient = healthAbsorptionAbility.MaxHealthCoefficient,
            };
        }

        private static Status CreateVampirismStatus(AbilityLevelData ability)
        {
            var vampirismAbility = (VampirismLevelData)ability;
            return new VampirismStatus
            {
                DamageCoefficient = vampirismAbility.DamageCoefficient,
            };
        }

        private static Status CreateBouncyWallStatus(AbilityLevelData ability)
        {
            var bouncyWallAbility = (BouncyWallLevelData)ability;
            return new BouncyWallStatus
            {
                CountLeft = bouncyWallAbility.Count,
            };
        }

        private static Status CreateRicochetStatus(AbilityLevelData ability)
        {
            var ricochetAbility = (RicochetLevelData)ability;
            return new RicochetStatus
            {
                CountLeft = ricochetAbility.Count,
            };
        }
    }
}