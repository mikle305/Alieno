using GameFlow.Context;
using Services;

namespace GameFlow.States
{
    public class RoomSelectionState : State
    {
        private readonly GameStateMachine _context;
        private readonly ObjectsProvider _objectsProvider;
        private readonly LevelMapService _levelMapService;


        public RoomSelectionState(GameStateMachine context)
        {
            _context = context;
            _objectsProvider = ObjectsProvider.Instance;
            _levelMapService = LevelMapService.Instance;
        }

        public override void Enter()
        {
            _levelMapService.AnimationFinished += EnterRoomLoadingState;
            _objectsProvider.RoomsMap.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            _levelMapService.AnimationFinished -= EnterRoomLoadingState;
            _objectsProvider.RoomsMap.gameObject.SetActive(false);
        }

        private void EnterRoomLoadingState()
            => _context.Enter<RoomLoadingState>();
    }
}