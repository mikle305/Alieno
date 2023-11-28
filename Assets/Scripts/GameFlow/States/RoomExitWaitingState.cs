using System;
using GameFlow.Context;
using GamePlay.Player;
using Services;

namespace GameFlow.States
{
    public class RoomExitWaitingState : State
    {
        private readonly GameStateMachine _context;
        private readonly ObjectsProvider _objectsProvider;


        public RoomExitWaitingState(GameStateMachine context)
        {
            _context = context;
            _objectsProvider = ObjectsProvider.Instance;
        }

        public override void Enter<TNext>()
            => InitPlayerExitDetector(EnterNextState<TNext>);

        public override void Exit()
            => DisposeRoomDependentObjects();
        
        private void DisposeRoomDependentObjects()
        {
            _objectsProvider.Character.gameObject.SetActive(false);
            _objectsProvider.Marker.gameObject.SetActive(false);
            _objectsProvider.Hud.gameObject.SetActive(false);
            UnityEngine.Object.Destroy(_objectsProvider.CurrentRoom.gameObject);
        }

        private void InitPlayerExitDetector(Action onPlayerExited)
        {
            var detector = _objectsProvider.CurrentRoom.ExitPoint.AddComponent<PlayerCollisionDetector>();
            detector.PlayerEntered += onPlayerExited;
        }

        private void EnterNextState<TNext>()
            where TNext : State
            => _context.Enter<TNext>();
    }
}