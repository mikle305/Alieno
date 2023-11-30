namespace GamePlay.Stats
{
    public class DefaultStat : IStat
    {
        private float _value;
        

        public DefaultStat(float value = 0.0f)
        {
            _value = value;
        }

        public float GetValue() 
            => _value;

        public void SetValue(float value)
        {
            _value = value;
        }
    }
}