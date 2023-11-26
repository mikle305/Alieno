using GamePlay.Characteristics;
using GamePlay.StatsSystem;

namespace GamePlay.Abilities
{
    public class HealthBuffComponent : AbilityComponent<HealthBoostData, HealthBoostLevelData>
    {
        private HealthData _healthData;
        private StatModifier _lastModifier;


        protected override void OnCreate()
        {
            _healthData = Entity.GetComponent<HealthData>();
            SetCurrentLevelBuff();
        }

        protected override void OnLevelUp()
        {
            SetCurrentLevelBuff();
        }

        private void SetCurrentLevelBuff()
        {
            float coefficient = CurrentLevel.PercentsBuff / 100.0f;
            var newModifier = new StatModifier(ModifierType.Coefficient, coefficient);

            if (CurrentLevelId == 1)
                _healthData.AddModifier(newModifier);
            else
                _healthData.ReplaceModifier(_lastModifier, newModifier);
            
            _lastModifier = newModifier;
        }
    }
}