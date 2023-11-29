using System;
using System.Collections.Generic;

namespace GamePlay.StatsSystem
{
    public class ModifiableStat : IStat, IModifications
    {
        private readonly List<StatModifier> _modifiers;
        private float _baseValue;
        private float _finalValue;


        public ModifiableStat(float baseValue = 0.0f)
        {
            _modifiers = new List<StatModifier>();
            _baseValue = baseValue;
            _finalValue = CalculateFinalValue();
        }

        public float GetValue() 
            => _finalValue;
        
        public float BaseValue
        {
            get => _baseValue;
            set
            {
                _baseValue = value;
                _finalValue = CalculateFinalValue();
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            _finalValue = CalculateFinalValue();
        }

        public bool RemoveModifier(StatModifier modifier)
        {
            bool result = _modifiers.Remove(modifier);
            _finalValue = CalculateFinalValue();
            return result;
        }

        public bool ReplaceModifier(StatModifier toRemove, StatModifier toAdd)
        {
            if (!RemoveModifier(toRemove))
                return false;
            
            AddModifier(toAdd);
            return true;
        }

        private float CalculateFinalValue()
        {
            CalculateModifiers(out float additionBefore, out float coefficient, out float additionAfter);
            float modifiedValue = (_baseValue + additionBefore) * coefficient + additionAfter;
            
            return MathF.Round(modifiedValue, 2);
        }

        private void CalculateModifiers(out float additionBefore, out float coefficient, out float additionAfter)
        {
            additionBefore = 0;
            additionAfter = 0;
            coefficient = 1;
            
            foreach (StatModifier modifier in _modifiers)
            {
                switch (modifier.Type)
                {
                    case ModifierType.AdditionBefore:
                        additionBefore += modifier.Value;
                        break;
                    case ModifierType.Coefficient:
                        coefficient += modifier.Value;
                        break;
                    case ModifierType.AdditionAfter:
                        additionAfter += modifier.Value;
                        break;
                    default:
                        throw new Exception("Unhandled modifier type");
                }
            }
        }
    }
}