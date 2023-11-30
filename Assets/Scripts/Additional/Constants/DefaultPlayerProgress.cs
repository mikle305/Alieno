using System.Collections.Generic;
using GamePlay.Abilities;

namespace Additional.Constants
{
    public static class DefaultPlayerProgress
    {
        public const float Health = 100;
        public const int Level = 1;
        public const int Room = 1;

        public const int SelectionAbilitiesCount = 3;

        public static Dictionary<AbilityId, int> GetAbilities()
            => new()
            {
                { AbilityId.ForwardShot, 1 },
            };
    }
}