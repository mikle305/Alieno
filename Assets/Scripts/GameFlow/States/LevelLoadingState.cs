using Additional.Constants;
using Cysharp.Threading.Tasks;
using GameFlow.Context;
using Services;

namespace GameFlow.States
{
    public class LevelLoadingState : State
    {
        private readonly GameStateMachine _context;
        private readonly SceneLoader _sceneLoader;
        private readonly MusicService _musicService;
        private readonly ObjectsProvider _objectsProvider;


        public LevelLoadingState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _musicService = MusicService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
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
            HudFactory hudFactory = HudFactory.Instance;
            hudFactory.CreateHud(_objectsProvider.Character);
        }
    }
}