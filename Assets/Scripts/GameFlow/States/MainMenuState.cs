using Additional.Constants;
using GameFlow.Context;
using Services;
using StaticData.Music;

namespace GameFlow.States
{
    public class MainMenuState : State
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly MenuService _menuService;
        private readonly MusicService _musicService;
        
        public MainMenuState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = SceneLoader.Instance;
            _menuService = MenuService.Instance;
            _musicService = MusicService.Instance;
        }

        private void SetMenuMusic()
        {
            _musicService.Play(MusicId.MainMenu1);
        }
        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.MainMenu,SetMenuMusic);
        }

        public override void Update()
        {
            if (_menuService.IsPlayInvoked())
                _gameStateMachine.Enter<SceneLoadingState>();
        }
    }
}