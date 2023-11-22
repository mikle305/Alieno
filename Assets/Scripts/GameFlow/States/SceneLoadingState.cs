using Additional.Constants;
using Cysharp.Threading.Tasks;
using GameFlow.Context;
using Services;
using UI.GamePlay;
using UnityEngine;

namespace GameFlow.States
{
    public class SceneLoadingState : State
    {
        private readonly GameStateMachine _context;
        private readonly SceneLoader _sceneLoader;
        private readonly MusicService _musicService;
        private readonly ObjectsProvider _objectsProvider;
        private GameFactory _gameFactory;
        private HudFactory _hudFactory;


        public SceneLoadingState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _musicService = MusicService.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.Level, OnLevelLoaded);
        }

        public override void Exit()
        {
        }

        private void OnLevelLoaded()
            => OnLevelLoadedAsync().Forget();

        private async UniTask OnLevelLoadedAsync()
        {
            await UniTask.DelayFrame(2);
            InitFactories();
            
            InitCharacter();
            InitMarker();
            InitHud();
        }

        private void InitFactories()
        {
            _gameFactory = GameFactory.Instance;
            _hudFactory = HudFactory.Instance;
        }
        
        private void InitCharacter()
        {
            GameObject character = _gameFactory.CreateCharacter();
            character.SetActive(false);
            _objectsProvider.Character = character;
        }

        private void InitHud()
        {
            GameObject character = _objectsProvider.Character;
            Hud hud = _hudFactory.Create(character);
            hud.gameObject.SetActive(false);
            _objectsProvider.Hud = hud;
        }

        private void InitMarker()
        {
            GameObject marker = _gameFactory.CreateMarker();
            marker.SetActive(false);
            _objectsProvider.Marker = marker;
        }
    }
}