using System;
using GamePlay.Abilities;

namespace SaveData
{
    [Serializable]
    public class PlayerData
    {
        public int Level = 1;
        public int Room = 1;

        public AbilityEntry[] Abilities =
        {
            new() { Id = AbilityId.ForwardShot, Level = 1 },
        };
    }
}