using System;
using Additional.Constants;
using GameFlow.Context;
using GamePlay.Abilities;
using SaveData;
using Services;
using Services.Save;

namespace GameFlow.States
{
    public class DefeatState : State
    {
        private readonly GameStateMachine _context;
        private readonly ObjectsProvider _objectsProvider;
        private readonly SaveService _saveService;
        private readonly EndGameMenuService _endGameMenuService;


        public DefeatState(GameStateMachine context)
        {
            _context = context;
            _objectsProvider = ObjectsProvider.Instance;
            _saveService = SaveService.Instance;
            _endGameMenuService = EndGameMenuService.Instance;
        }

        public override void Enter()
        {
            ResetLevelProgress();
            SubscribeToMainMenu();
            ClearLevelObjects();
        }

        public override void Exit() 
            => _endGameMenuService.ToMainMenuInvoked -= EnterMainMenu;

        private void ResetLevelProgress()
        {
            PlayerData playerProgress = _saveService.Progress.PlayerData;
            playerProgress.AbilitySelected = true;
            playerProgress.GeneratedAbilities = Array.Empty<AbilityId>();
            playerProgress.CurrentAbilities = DefaultPlayerProgress.GetAbilities();
            playerProgress.CurrentHealth = DefaultPlayerProgress.Health;
            playerProgress.Room = DefaultPlayerProgress.Room;
        }

        private void SubscribeToMainMenu()
        {
            _endGameMenuService.ToMainMenuInvoked += EnterMainMenu;
            _endGameMenuService.InvokeDefeat();
        }

        private void ClearLevelObjects()
        {
            _objectsProvider.Hud.gameObject.SetActive(false);
        }

        private void EnterMainMenu() 
            => _context.Enter<MainMenuState>();
    }
}