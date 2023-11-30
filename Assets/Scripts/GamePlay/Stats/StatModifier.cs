namespace GamePlay.Stats
{
    public class StatModifier
    {
        /// <summary>
        /// With "coefficient" type, value 0.1 equals +10% and -0.1 equals -10%
        /// </summary>
        public StatModifier(ModifierType type, float value)
        {
            Value = value;
            Type = type;
        }

        public float Value { get; }

        public ModifierType Type { get; }
    }
}