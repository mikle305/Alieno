using System;
using GameFlow.Context;
using GamePlay.Abilities;
using SaveData;
using Services;
using Services.Save;

namespace GameFlow.States
{
    public class AbilitySelectionState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;
        private readonly AbilitySelectionService _abilitySelectionService;
        private readonly ObjectsProvider _objectsProvider;


        public AbilitySelectionState(GameStateMachine context)
        {
            _context = context;
            _saveService = SaveService.Instance;
            _abilitySelectionService = AbilitySelectionService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
        }

        public override void Enter()
        {
            _abilitySelectionService.AbilitySelected += OnAbilitySelected;
            GenerateAndSaveAbilities();
        }

        public override void Exit()
        {
            _abilitySelectionService.AbilitySelected -= OnAbilitySelected;
        }

        private void OnAbilitySelected(AbilityId id)
        {
            SaveAbilitiesProgress(id);
            SetAbilityToEntity(id);
            EnterRoomSelection();
        }

        private void GenerateAndSaveAbilities()
        {
            PlayerData playerProgress = _saveService.Progress.PlayerData;
            playerProgress.SelectionAbilities = _abilitySelectionService
                .GenerateAbilities(playerProgress.CurrentAbilities);
            
            _saveService.Save();
        }

        private void SaveAbilitiesProgress(AbilityId id)
        {
            PlayerData playerProgress = _saveService.Progress.PlayerData;
            if (playerProgress.CurrentAbilities.ContainsKey(id))
                playerProgress.CurrentAbilities[id] += 1;
            else
                playerProgress.CurrentAbilities[id] = 1;

            playerProgress.SelectionAbilities = Array.Empty<AbilityId>();
            _saveService.Save();
        }

        private void SetAbilityToEntity(AbilityId id)
        {
            var characterEntity = _objectsProvider.Character.GetComponent<AbilitiesEntity>();
            if (characterEntity.AbilitiesMap.ContainsKey(id))
                characterEntity.UpLevel(id);
            else
                characterEntity.AddAbility(id);
        }

        private void EnterRoomSelection() 
            => _context.Enter<RoomSelectionState>();
    }
}