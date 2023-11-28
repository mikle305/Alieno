using GameFlow.Context;
using GamePlay.Abilities;
using SaveData;
using Services;
using Services.Save;

namespace GameFlow.States
{
    public class AbilitiesGenerationState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;
        private readonly AbilitySelectionService _abilitySelectionService;


        public AbilitiesGenerationState(GameStateMachine context)
        {
            _context = context;
            _saveService = SaveService.Instance;
            _abilitySelectionService = AbilitySelectionService.Instance;
        }

        public override void Enter()
        {
            if (IsAbilitiesNotGenerated())
                GenerateAbilities();
            else
                RestoreAbilities();

            EnterAbilitySelection();
        }

        private void GenerateAbilities()
        {
            PlayerData playerProgress = _saveService.Progress.PlayerData;
            playerProgress.GeneratedAbilities = _abilitySelectionService.GenerateAbilities(playerProgress.CurrentAbilities);
            playerProgress.AbilitySelected = false;
            _saveService.Save();
        }

        private void RestoreAbilities()
        {
            AbilityId[] generatedAbilities = _saveService.Progress.PlayerData.GeneratedAbilities;
            _abilitySelectionService.RestoreAbilities(generatedAbilities);
        }

        private bool IsAbilitiesNotGenerated() 
            => _saveService.Progress.PlayerData.GeneratedAbilities.Length == 0;

        private void EnterAbilitySelection() 
            => _context.Enter<AbilitySelectionState>();
    }
}