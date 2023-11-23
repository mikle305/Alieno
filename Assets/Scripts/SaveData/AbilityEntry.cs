using System;
using GamePlay.Abilities;

namespace SaveData
{
    [Serializable]
    public class AbilityEntry
    {
        public AbilityId Id;
        public int Level = 1;
    }
}