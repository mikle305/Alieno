using System;
using Additional.Constants;
using Additional.Utils;
using GamePlay.Stats;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class Characteristic : MonoBehaviour
    {
        private DefaultStat _current;
        private ModifiableStat _max;

        public float Current => _current.GetValue();
        public float Max => _max.GetValue();

        public event Action ValueChanged;
        public event Action ZeroReached;
        public event Action<float> DecreaseTried;
        public event Action<float> IncreaseTried;


        public void Init(float current, float max)
        {
            _max = new ModifiableStat(max);
            _current = new DefaultStat(current);
        }

        public void Increase(float value)
        {
            ValidateLessThanZero(value);
            IncreaseTried?.Invoke(value);
            
            if (IsZero())
                return;

            if (IsFull())
                return;

            ApplyIncrease(value);
        }

        public void Decrease(float value)
        {
            ValidateLessThanZero(value);
            DecreaseTried?.Invoke(value);
            
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
            _max.ReplaceModifier(toRemove, toAdd);
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
            {
                DecreaseTried?.Invoke(-diff);
                ApplyDecrease(-diff);
            }
            else if (diff > 0)
            {
                IncreaseTried?.Invoke(diff);
                ApplyIncrease(diff);
            }
        }
    }
}