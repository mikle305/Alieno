namespace GamePlay.StatsSystem
{
    public interface IStat
    {
        /// <summary>
        /// Returns final value of stat.
        /// </summary>
        /// <returns></returns>
        public float GetValue();
    }
}