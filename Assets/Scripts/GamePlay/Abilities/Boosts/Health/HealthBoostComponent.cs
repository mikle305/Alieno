using GamePlay.Characteristics;
using GamePlay.StatsSystem;

namespace GamePlay.Abilities
{
    public class HealthBoostComponent : AbilityComponent<HealthBoostData, HealthBoostLevelData>
    {
        private HealthData _healthData;
        private StatModifier _lastModifier;


        protected override void OnCreate()
        {
            _healthData = Entity.GetComponent<HealthData>();
            SetCurrentLevelBoost();
        }

        protected override void OnLevelChanged()
        {
            SetCurrentLevelBoost();
        }

        private void SetCurrentLevelBoost()
        {
            var newModifier = new StatModifier(ModifierType.Coefficient, CurrentLevel.HealthCoefficient - 1);

            if (_lastModifier == null)
                _healthData.AddModifier(newModifier);
            else
                _healthData.ReplaceModifier(_lastModifier, newModifier);
            
            _lastModifier = newModifier;
        }
    }
}