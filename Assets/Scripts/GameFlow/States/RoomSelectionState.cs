using Services;
using Services.Save;
using StaticData.Music;

namespace GameFlow.States
{
    public class RoomSelectionState : State
    {
        private readonly GameStateMachine _context;
        private readonly ObjectsProvider _objectsProvider;
        private readonly LevelMapService _levelMapService;
        private readonly MusicService _musicService;
        private readonly SaveService _saveService;


        public RoomSelectionState(GameStateMachine context, MusicService musicService, LevelMapService levelMapService,
            ObjectsProvider objectsProvider, SaveService saveService)
        {
            _context = context;
            _levelMapService = levelMapService;
            _objectsProvider = objectsProvider;
            _musicService = musicService;
            _saveService = saveService;
        }

        public override void Enter()
        {
            _musicService.Play(MusicId.PerkSelection);
            _levelMapService.AnimationFinished += EnterRoomLoadingState;
            _levelMapService.SetRoom(_saveService.Progress.PlayerData.Room - 2);
        }

        public override void Exit()
        {
            _levelMapService.AnimationFinished -= EnterRoomLoadingState;
            _objectsProvider.RoomsMap.gameObject.SetActive(false);
            _objectsProvider.RoomsMap.NextLvlButton.onClick.RemoveListener(_levelMapService.DisplayNextRoom);
        }

        private void EnterRoomLoadingState()
            => _context.Enter<RoomLoadingState>();
    }
}