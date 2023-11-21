using GamePlay.Characteristics;
using GamePlay.StatsSystem;

namespace GamePlay.Abilities
{
    public class HealthBuffComponent : AbilityComponent<HealthBuffData, HealthBuffLevelData>
    {
        private Health _health;
        private StatModifier _lastModifier;


        protected override void OnCreate()
        {
            _health = Entity.GetComponent<Health>();
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
                _health.AddModifier(newModifier);
            else
                _health.ReplaceModifier(_lastModifier, newModifier);
            
            _lastModifier = newModifier;
        }
    }
}