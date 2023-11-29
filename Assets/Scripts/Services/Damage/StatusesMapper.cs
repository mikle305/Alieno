using System;
using System.Collections.Generic;
using Additional.Game;
using GamePlay.Abilities;
using GamePlay.Damage;

namespace Services.Damage
{
    public class StatusesMapper : MonoSingleton<StatusesMapper>
    {
        private readonly Dictionary<Type, Func<AbilityLevelData, Status>> _factoriesMap = new()
        {
            { typeof(FlameStatusLevelData), CreateFlameStatus },
            { typeof(PoisonStatusLevelData), CreatePoisonStatus },
            { typeof(ObstaclePenetrationLevelData), CreateObstaclePenetrationStatus },
        };

        private readonly Dictionary<Type, Type> _typesMap = new()
        {
            { typeof(FlameStatusLevelData), typeof(FlameStatus) },
            { typeof(PoisonStatusLevelData), typeof(PoisonStatus) },
            { typeof(ObstaclePenetrationLevelData), typeof(ObstaclePenetrationStatus) },
        };

        public Status Map(AbilityLevelData abilityLevel, out Type statusType)
        {
            Type abilityType = abilityLevel.GetType();
            statusType = _typesMap[abilityType];
            return _factoriesMap[abilityType].Invoke(abilityLevel);
        }

        private static Status CreateFlameStatus(AbilityLevelData abilityLevel)
        {
            var flameAbility = (FlameStatusLevelData)abilityLevel;
            return new FlameStatus
            {
                DamagePercents = flameAbility.DamagePercents,
                Rate = flameAbility.Rate,
                CountLeft = flameAbility.Count,
            };
        }

        private static Status CreatePoisonStatus(AbilityLevelData abilityLevel)
        {
            var poisonAbility = (PoisonStatusLevelData)abilityLevel;
            return new PoisonStatus
            {
                DamagePercents = poisonAbility.DamagePercents,
                Rate = poisonAbility.Rate,
            };
        }

        private static Status CreateObstaclePenetrationStatus(AbilityLevelData abilityLevel)
        {
            var obstaclePenetrationAbility = (ObstaclePenetrationLevelData) abilityLevel;
            return new ObstaclePenetrationStatus
            {
                CountLeft = obstaclePenetrationAbility.Count,
            };
        }
    }
}