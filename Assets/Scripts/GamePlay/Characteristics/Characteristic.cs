using System;
using Additional.Constants;
using Additional.Utils;
using GamePlay.StatsSystem;

namespace GamePlay.Characteristics
{
    public class Characteristic
    {
        private readonly DefaultStat _current;
        private readonly ModifiableStat _max;

        public float Current => _current.GetValue();
        public float Max => _max.GetValue();
        public IModifications Modifications => _max;
        
        public event Action ValueChanged;
        public event Action ZeroReached;


        public Characteristic(float current, float max)
        {
            _max = new ModifiableStat(max);
            _current = new DefaultStat(current);
        }

        public void Increase(float health)
        {
            ValidateLessThanZero(health);
            if (IsZero())
                return;
            
            if (IsFull())
                return;
            
            ApplyIncrease(health);
            InvokeChanged();
        }

        public void Decrease(float damage)
        {
            ValidateLessThanZero(damage);
            if (IsZero())
                return;
            
            ApplyDecrease(damage);
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