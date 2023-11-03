using Additional.Constants;
using GameFlow.Context;
using Services;

namespace GameFlow.States
{
    public class MainMenuState : State
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly MenuService _menuService;


        public MainMenuState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = SceneLoader.Instance;
            _menuService = MenuService.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.MainMenu);
        }

        public override void Update()
        {
            if (_menuService.IsPlayInvoked())
                _gameStateMachine.Enter<LevelLoadingState>();
        }
    }
}