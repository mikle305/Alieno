using System;
using Additional.Constants;
using Additional.Utils;
using GamePlay.StatsSystem;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class Characteristic : MonoBehaviour
    {
        [SerializeField] private float _defaultCurrent;
        [SerializeField] private float _defaultMax;
        
        private DefaultStat _current;
        private ModifiableStat _max;

        public float Current => _current.GetValue();
        public float Max => _max.GetValue();

        public event Action ValueChanged;
        public event Action ZeroReached;


        private void Awake()
        {
            _max = new ModifiableStat(_defaultMax);
            _current = new DefaultStat(_defaultCurrent);
        }

        public void Increase(float value)
        {
            ValidateLessThanZero(value);
            if (IsZero())
                return;

            if (IsFull())
                return;

            ApplyIncrease(value);
        }

        public void Decrease(float value)
        {
            print($"Damage: {value}");
            ValidateLessThanZero(value);
            if (IsZero())
                return;

            ApplyDecrease(value);
        }

        public void AddModifier(StatModifier modifier)
        {
            float oldValue = Max;
            _max.AddModifier(modifier);
            float diff = Max - oldValue;
            
            ChangeCurrent(diff);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _max.RemoveModifier(modifier);
            if (Current > Max)
            {
                _current.SetValue(Max);
                ValueChanged?.Invoke();
            }
        }

        public void ReplaceModifier(StatModifier toRemove, StatModifier toAdd)
        {
            float diff = -Max;
            _max.RemoveModifier(toRemove);
            _max.AddModifier(toAdd);
            diff += Max;
            
            ChangeCurrent(diff);
        }

        private void ApplyIncrease(float value)
        {
            float sum = _current.GetValue() + value;
            float maxValue = _max.GetValue();
            _current.SetValue(sum > maxValue ? maxValue : sum);
            ValueChanged?.Invoke();
        }

        private void ApplyDecrease(float value)
        {
            float diff = _current.GetValue() - value;
            if (diff > 0)
            {
                _current.SetValue(diff);
            }
            else
            {
                _current.SetValue(0);
                ZeroReached?.Invoke();
            }
            
            ValueChanged?.Invoke();
        }

        private bool IsFull()
            => Math.Abs(Max - Current) < Constants.Epsilon;

        private bool IsZero()
            => Current == 0;

        private static void ValidateLessThanZero(float value)
        {
            if (value <= 0)
                ThrowUtils.ValueLessThanZero();
        }

        private void ChangeCurrent(float diff)
        {
            if (diff < 0)
                ApplyDecrease(-diff);
            else if (diff > 0)
                ApplyIncrease(diff);
        }
    }
}