namespace GamePlay.StatsSystem
{
    public interface IModifications
    {
        public float BaseValue { get; set; }
        public void AddModifier(StatModifier modifier);
        public bool RemoveModifier(StatModifier modifier);
    }
}