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


        public DefeatState(
            GameStateMachine context,
            ObjectsProvider objectsProvider, 
            SaveService saveService,
            EndGameMenuService endGameMenuService)
        {
            _context = context;
            _objectsProvider = objectsProvider;
            _saveService = saveService;
            _endGameMenuService = endGameMenuService;
        }

        public override void Enter()
        {
            ResetRoomsProgress();
            SubscribeToMainMenu();
            ClearLevelObjects();
        }

        public override void Exit() 
            => _endGameMenuService.ToMainMenuInvoked -= EnterMainMenu;

        private void ResetRoomsProgress()
        {
            _saveService.ResetRoomsProgress();
            _saveService.Save();
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