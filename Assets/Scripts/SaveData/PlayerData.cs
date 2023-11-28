using System;
using System.Collections.Generic;
using Additional.Constants;
using GamePlay.Abilities;

namespace SaveData
{
    [Serializable]
    public class PlayerData
    {
        public int Level = DefaultPlayerProgress.Level;
        public int Room = DefaultPlayerProgress.Room;
        public float CurrentHealth = DefaultPlayerProgress.Health;
        public Dictionary<AbilityId, int> CurrentAbilities = DefaultPlayerProgress.GetAbilities();

        public bool AbilitySelected = true;
        public AbilityId[] GeneratedAbilities = Array.Empty<AbilityId>();
    }
}