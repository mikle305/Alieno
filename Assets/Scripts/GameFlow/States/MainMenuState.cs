using Additional.Constants;
using GameFlow.Context;
using Services;
using Services.Notifications;
using Services.Save;
using StaticData.Music;

namespace GameFlow.States
{
    public class MainMenuState : State
    {
        private readonly GameStateMachine _context;
        private readonly SceneLoader _sceneLoader;
        private readonly MenuService _menuService;
        private readonly MusicService _musicService;
        private readonly StaticDataService _staticDataService;
        private readonly SaveService _saveService;
        private readonly MessageNotifier _messageNotifier;


        public MainMenuState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _menuService = MenuService.Instance;
            _musicService = MusicService.Instance;
            _staticDataService = StaticDataService.Instance;
            _saveService = SaveService.Instance;
            _messageNotifier = MessageNotifier.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.MainMenu, OnMainMenuLoaded);
        }

        public override void Exit()
        {
            _menuService.PlayInvoked -= TryEnterLevel;
        }

        private void OnMainMenuLoaded()
        {
            _menuService.PlayInvoked += TryEnterLevel;
            PlayMenuMusic();
        }

        private void TryEnterLevel()
        {
            if (IsLastLevel())
                ShowLastLevelPopup();
            else
                EnterSceneLoading();
        }

        private bool IsLastLevel() 
            => _staticDataService.GetPrefabsConfig().Levels.Length < _saveService.Progress.PlayerData.Level;

        private void PlayMenuMusic() 
            => _musicService.Play(MusicId.MainMenu1);

        private void ShowLastLevelPopup()
            => _messageNotifier.NotifyMessage(MessageId.NoLevelsMore);

        private void EnterSceneLoading() 
            => _context.Enter<SceneLoadingState>();
    }
}