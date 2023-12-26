using System;
using System.Collections.Generic;
using System.Linq;
using Additional.Constants;
using Additional.Game;
using GamePlay.Abilities;
using StaticData;
using StaticData.Abilities;

namespace Services
{
    public class AbilitySelectionService : MonoSingleton<AbilitySelectionService>
    {
        private StaticDataService _staticDataService;
        private RandomService _randomService;

        public event Action<AbilityId[]> AbilitiesGenerated;
        public event Action<AbilityId> AbilitySelected;


        private void Start()
        {
            _randomService = RandomService.Instance;
            _staticDataService = StaticDataService.Instance;
        }

        public AbilityId[] GenerateAbilities(Dictionary<AbilityId, int> currentAbilities)
        {
            IEnumerable<PlayerAbilityData> filteredAbilities = _staticDataService
                .GetAbilitiesConfig()
                .PlayerAbilitiesData
                .Where(a => !IsMaxLevel(currentAbilities, a));

            AbilityId[] generatedAbilities = _randomService
                .PickMany(filteredAbilities, DefaultPlayerProgress.SelectionAbilitiesCount)
                .Select(a => a.AbilityId)
                .ToArray();

            AbilitiesGenerated?.Invoke(generatedAbilities);
            return generatedAbilities;
        }

        public void RestoreAbilities(AbilityId[] generatedAbilities)
            => AbilitiesGenerated?.Invoke(generatedAbilities);

        public void SetSelectedAbility(AbilityId id) 
            => AbilitySelected?.Invoke(id);

        private static bool IsMaxLevel(IReadOnlyDictionary<AbilityId, int> currentAbilities, PlayerAbilityData playerAbility)
        {
            if (!currentAbilities.TryGetValue(playerAbility.AbilityId, out int currentLevel))
                return false;

            int[] availableLevels = playerAbility.Levels;
            for (var i = 0; i < availableLevels.Length - 1; i++)
            {
                if (currentLevel == availableLevels[i])
                    return false;
            }

            return true;
        }
    }
}