using System;

namespace SaveData
{
    [Serializable]
    public class Progress
    {
        public SettingsData SettingsData = new();
        public PlayerData PlayerData = new();
    }
}