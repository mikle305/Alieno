using System;
using System.Linq;
using GamePlay.Abilities;
using SaveData;
using Services;
using Services.Save;

namespace GameFlow.States
{
    public class AbilitySelectionState : State
    {
        private readonly GameStateMachine _context;
        private readonly AbilitySelectionService _abilitySelectionService;
        private readonly SaveService _saveService;
        private readonly ObjectsProvider _objectsProvider;
        private readonly StaticDataService _staticDataService;


        public AbilitySelectionState(
            GameStateMachine context, 
            AbilitySelectionService abilitySelectionService,
            StaticDataService staticDataService, 
            SaveService saveService, 
            ObjectsProvider objectsProvider)
        {
            _context = context;
            _abilitySelectionService = abilitySelectionService;
            _staticDataService = staticDataService;
            _saveService = saveService;
            _objectsProvider = objectsProvider;
        }

        public override void Enter()
        {
            _objectsProvider.Hud.gameObject.SetActive(true);
            _abilitySelectionService.AbilitySelected += OnAbilitySelected;
        }

        public override void Exit()
        {
            _objectsProvider.Hud.gameObject.SetActive(false);
            _abilitySelectionService.AbilitySelected -= OnAbilitySelected;
        }

        private void OnAbilitySelected(AbilityId id)
        {
            int level = GetNextLevelOfAbility(id);
            SaveAbilitiesProgress(id, level);
            SetPlayerEntityAbility(id, level);
            if (HasCurrentRoom())
                EnterRoomExitWaiting();
            else
                EnterRoomSelection();
        }

        private int GetNextLevelOfAbility(AbilityId id)
        {
            var currentLevel = 0;
            if (TryGetPlayerEntityAbility(id, out AbilityComponent abilityComponent))
                currentLevel = abilityComponent.CurrentLevelId;

            int[] availableLevels = GetAbilityAvailableLevels(id);
            if (currentLevel == 0)
                return availableLevels[0];

            for (int i = 0; i < availableLevels.Length; i++)
            {
                if (availableLevels[i] == currentLevel)
                    return availableLevels[i + 1];
            }

            return -1;
        }

        private void SaveAbilitiesProgress(AbilityId id, int level)
        {
            PlayerData playerProgress = _saveService.Progress.PlayerData;
            playerProgress.CurrentAbilities[id] = level;
            playerProgress.AbilitySelected = true;
            playerProgress.GeneratedAbilities = Array.Empty<AbilityId>();
            _saveService.Save();
        }

        private void SetPlayerEntityAbility(AbilityId id, int level)
        {
            var characterEntity = _objectsProvider.Character.GetComponent<AbilitiesEntity>();
            if (characterEntity.AbilitiesMap.ContainsKey(id))
                characterEntity.SetLevel(id, level);
            else
                characterEntity.AddAbility(id);
        }

        private bool HasCurrentRoom()
            => _objectsProvider.CurrentRoom != null;

        private void EnterRoomExitWaiting()
            => _context.Enter<RoomExitWaitingState, RoomSelectionState>();

        private void EnterRoomSelection()
            => _context.Enter<RoomSelectionState>();

        private bool TryGetPlayerEntityAbility(AbilityId id, out AbilityComponent abilityComponent)
            => _objectsProvider
                .Character
                .GetComponent<AbilitiesEntity>()
                .AbilitiesMap
                .TryGetValue(id, out abilityComponent);

        private int[] GetAbilityAvailableLevels(AbilityId id)
            => _staticDataService
                .GetAbilitiesConfig()
                .PlayerAbilitiesData
                .First(playerAbility => playerAbility.AbilityId == id)
                .Levels;
    }
}