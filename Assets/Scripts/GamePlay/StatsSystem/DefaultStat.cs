using System;

namespace GamePlay.StatsSystem
{
    public class DefaultStat : IStat
    {
        private float _value;
        
        public event Action<float, float> ValueChanged;
        

        public DefaultStat(float value = 0.0f)
        {
            _value = value;
        }

        public float GetValue() 
            => _value;

        public void SetValue(float value)
        {
            float oldValue = _value;
            _value = value;
            
            ValueChanged?.Invoke(oldValue, _value);
        }
    }
}