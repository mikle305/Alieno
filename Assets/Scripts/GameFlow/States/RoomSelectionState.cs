using GameFlow.Context;
using Services;
using StaticData.Music;

namespace GameFlow.States
{
    public class RoomSelectionState : State
    {
        private readonly GameStateMachine _context;
        private readonly ObjectsProvider _objectsProvider;
        private LevelMapService _levelMapService;
        private MusicService _musicService;


        public RoomSelectionState(GameStateMachine context)
        {
            _context = context;
            _objectsProvider = ObjectsProvider.Instance;
            _musicService = MusicService.Instance;
        }

        public override void Enter()
        {
            _levelMapService = LevelMapService.Instance;
            _levelMapService.AnimationFinished += EnterRoomLoadingState;
            _objectsProvider.RoomsMap.gameObject.SetActive(true);
            _objectsProvider.RoomsMap.NextLvlButton.onClick.AddListener(_levelMapService.DisplayNextRoom);
            _musicService.Play(MusicId.PerkSelection);
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