using System;
using UnityEngine.Serialization;

namespace SaveData
{
    [Serializable]
    public class PlayerProgress
    {
        public string AuthToken = string.Empty;
        public SettingsData SettingsData = new();
    }
}