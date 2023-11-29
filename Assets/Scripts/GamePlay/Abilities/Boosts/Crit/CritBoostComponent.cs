using GamePlay.Characteristics;
using GamePlay.StatsSystem;

namespace GamePlay.Abilities
{
    public class CritBoostComponent : AbilityComponent<CritBoostData, CritBoostLevelData>
    {
        private ProjectileAttackData _attackData;
        private StatModifier _lastDamageModifier;
        private StatModifier _lastChanceModifier;


        protected override void OnCreate()
        {
            _attackData = Entity.GetComponent<ProjectileAttackData>();
            SetCurrentLevelBoost();
        }

        protected override void OnLevelUp()
        {
            SetCurrentLevelBoost();
        }

        private void SetCurrentLevelBoost()
        {
            BoostCritDamage();
            BoostCritChance();
        }

        private void BoostCritDamage()
        {
            var damageModifier = new StatModifier(ModifierType.AdditionBefore, CurrentLevel.MultiplierCoefficient - 1);

            if (_lastDamageModifier == null)
                _attackData.CritMultiplier.AddModifier(damageModifier);
            else
                _attackData.CritMultiplier.ReplaceModifier(_lastDamageModifier, damageModifier);

            _lastDamageModifier = damageModifier;
        }        
        
        private void BoostCritChance()
        {
            var chanceModifier = new StatModifier(ModifierType.AdditionBefore, CurrentLevel.ChanceCoefficient);

            if (_lastDamageModifier == null)
                _attackData.CritMultiplier.AddModifier(chanceModifier);
            else
                _attackData.CritMultiplier.ReplaceModifier(_lastChanceModifier, chanceModifier);

            _lastChanceModifier = chanceModifier;
        }
    }
}