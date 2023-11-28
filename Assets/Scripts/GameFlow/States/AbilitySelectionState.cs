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
        private readonly AbilitySelectionService _abilitySelectionService;
        private readonly SaveService _saveService;
        private readonly ObjectsProvider _objectsProvider;
        

        public AbilitySelectionState(GameStateMachine context)
        {
            _context = context;
            _abilitySelectionService = AbilitySelectionService.Instance;
            _saveService = SaveService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
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
            SaveAbilitiesProgress(id);
            UpPlayerAbility(id);
            EnterRoomSelection();
        }

        private void SaveAbilitiesProgress(AbilityId id)
        {
            PlayerData playerProgress = _saveService.Progress.PlayerData;
            if (playerProgress.CurrentAbilities.ContainsKey(id))
                playerProgress.CurrentAbilities[id] += 1;
            else
                playerProgress.CurrentAbilities[id] = 1;

            playerProgress.AbilitySelected = true;
            playerProgress.GeneratedAbilities = Array.Empty<AbilityId>();
            _saveService.Save();
        }

        private void UpPlayerAbility(AbilityId id)
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