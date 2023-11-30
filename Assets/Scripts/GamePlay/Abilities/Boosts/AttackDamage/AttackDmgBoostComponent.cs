using GamePlay.Characteristics;
using GamePlay.Stats;

namespace GamePlay.Abilities
{
    public class AttackDmgBoostComponent : AbilityComponent<AttackDmgBoostData, AttackDmgBoostLevelData>
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
            BoostAttackDamage();
        }

        private void BoostAttackDamage()
        {
            var newModifier = new StatModifier(ModifierType.Coefficient, CurrentLevel.MultiplierCoefficient - 1);

            if (_lastModifier == null)
                _attackData.AttackDamage.AddModifier(newModifier);
            else
                _attackData.AttackDamage.ReplaceModifier(_lastModifier, newModifier);

            _lastModifier = newModifier;
        }
    }
}