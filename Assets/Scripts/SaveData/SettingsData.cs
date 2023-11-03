using System;

namespace SaveData
{
    [Serializable]
    public class SettingsData
    {
        public float Volume;

        public SettingsData()
        {
            Volume = 1;
        }
    }
}