using GamePlay.Characteristics;
using GamePlay.Stats;

namespace GamePlay.Abilities
{
    public class AttackSpdBoostComponent : AbilityComponent<AttackSpdBoostData, AttackSpdBoostLevelData>
    {
        private ProjectileAttackData _attackData;
        private StatModifier _lastModifier;
        private StatModifier _lastChanceModifier;


        protected override void OnCreate()
        {
            _attackData = Entity.GetComponent<ProjectileAttackData>();
            SetCurrentLevelBoost();
        }

        protected override void OnLevelChanged()
        {
            SetCurrentLevelBoost();
        }

        private void SetCurrentLevelBoost()
        {
            BoostAttackSpeed();
        }

        private void BoostAttackSpeed()
        {
            var newModifier = new StatModifier(ModifierType.Coefficient, CurrentLevel.RateCoefficient - 1);

            if (_lastModifier == null)
                _attackData.AttackRate.AddModifier(newModifier);
            else
                _attackData.AttackRate.ReplaceModifier(_lastModifier, newModifier);

            _lastModifier = newModifier;
        }
    }
}