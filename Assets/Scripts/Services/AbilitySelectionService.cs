using System;
using System.Collections.Generic;
using System.Linq;
using Additional.Constants;
using Additional.Game;
using GamePlay.Abilities;

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
            var skipAbilities = new HashSet<AbilityId>();
            IEnumerable<AbilityData> filteredAbilities = _staticDataService
                .GetAllAbilities()
                .Where(a => !IsMaxLevel(currentAbilities, a));

            AbilityId[] generatedAbilities = _randomService
                .PickMany(filteredAbilities, DefaultPlayerProgress.SelectionAbilitiesCount)
                .Select(a => a.Id)
                .ToArray();

            AbilitiesGenerated?.Invoke(generatedAbilities);
            return generatedAbilities;
        }

        public void SetSelectedAbility(AbilityId id)
            => AbilitySelected?.Invoke(id);

        private static bool IsMaxLevel(Dictionary<AbilityId,int> currentAbilities, AbilityData targetAbility)
        {
            if (!currentAbilities.TryGetValue(targetAbility.Id, out int currentLevel))
                return false;

            return currentLevel == targetAbility.MaxLevel;
        }
    }
}