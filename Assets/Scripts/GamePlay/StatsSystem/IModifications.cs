namespace GamePlay.StatsSystem
{
    public interface IModifications
    {
        public void AddModifier(StatModifier modifier);
        public bool RemoveModifier(StatModifier modifier);
    }
}