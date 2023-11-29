using GamePlay.Characteristics;
using GamePlay.StatsSystem;

namespace GamePlay.Abilities
{
    public class DashBoostComponent : AbilityComponent<DashBoostData, DashBoostLevelData>
    {
        private DashData _dashData;
        private StatModifier _lastRateModifier;
        private StatModifier _lastChanceModifier;
        private StatModifier _lastSpeedModifier;


        protected override void OnCreate()
        {
            _dashData = Entity.GetComponent<DashData>();
            SetCurrentLevelBoost();
        }

        protected override void OnLevelUp()
        {
            SetCurrentLevelBoost();
        }

        private void SetCurrentLevelBoost()
        {
            BoostDashRate();
            BoostDashSpeed();
        }

        private void BoostDashRate()
        {
            var rateModifier = new StatModifier(ModifierType.Coefficient, CurrentLevel.RateCoefficient - 1);

            if (_lastRateModifier == null)
                _dashData.Rate.AddModifier(rateModifier);
            else
                _dashData.Rate.ReplaceModifier(_lastRateModifier, rateModifier);

            _lastRateModifier = rateModifier;
        }        
        
        private void BoostDashSpeed()
        {
            var speedModifier = new StatModifier(ModifierType.Coefficient, CurrentLevel.MoveSpeedCoefficient - 1);

            if (_lastRateModifier == null)
                _dashData.Speed.AddModifier(speedModifier);
            else
                _dashData.Speed.ReplaceModifier(_lastSpeedModifier, speedModifier);

            _lastSpeedModifier = speedModifier;
        }
    }
}