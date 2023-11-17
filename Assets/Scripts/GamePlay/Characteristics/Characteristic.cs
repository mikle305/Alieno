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
        public IModifications Modifications => _max;

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
            InvokeChanged();
        }

        public void Decrease(float value)
        {
            ValidateLessThanZero(value);
            if (IsZero())
                return;

            ApplyDecrease(value);
            InvokeChanged();
            TryInvokeZeroReached();
        }

        private void ApplyIncrease(float health)
        {
            float sum = _current.GetValue() + health;
            float maxValue = _max.GetValue();
            _current.SetValue(sum > maxValue ? maxValue : sum);
        }

        private void ApplyDecrease(float damage)
        {
            float diff = _current.GetValue() - damage;
            if (diff > 0)
                _current.SetValue(diff);
            else
                _current.SetValue(0);
        }

        private bool IsFull()
            => Math.Abs(Max - Current) < Constants.Epsilon;

        private bool IsZero()
            => Current == 0;

        private void TryInvokeZeroReached()
        {
            if (IsZero())
                ZeroReached?.Invoke();
        }

        private void InvokeChanged()
            => ValueChanged?.Invoke();

        private static void ValidateLessThanZero(float value)
        {
            if (value <= 0)
                ThrowUtils.ValueLessThanZero();
        }
    }
}