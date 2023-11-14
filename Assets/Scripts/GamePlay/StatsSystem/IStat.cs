using System;

namespace GamePlay.StatsSystem
{
    public interface IStat
    {
        /// <summary>
        /// Returns final value of stat.
        /// </summary>
        /// <returns></returns>
        public float GetValue();

        /// <summary>
        /// Notify subscribers with 2 params (value before change, value after change).
        /// </summary>
        public event Action<float, float> ValueChanged;
    }
}