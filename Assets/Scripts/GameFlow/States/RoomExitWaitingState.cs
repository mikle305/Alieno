using System;
using GameFlow.Context;
using GamePlay.Player;
using Services;
using UnityEngine;

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
        {
            EnableExitDirectionArrow();
            InitPlayerExitDetector(EnterNextState<TNext>);
        }

        public override void Exit()
            => DisposeRoomDependentObjects();

        private void EnableExitDirectionArrow()
        {
            Transform exitPoint = _objectsProvider.CurrentRoom.ExitPoint.transform;
            _objectsProvider.DirectionArrow.SetTarget(exitPoint);
            _objectsProvider.DirectionArrow.gameObject.SetActive(true);
        }

        private void InitPlayerExitDetector(Action onPlayerExited)
        {
            var detector = _objectsProvider.CurrentRoom.ExitPoint.AddComponent<PlayerCollisionDetector>();
            detector.PlayerEntered += onPlayerExited;
        }

        private void DisposeRoomDependentObjects()
        {
            _objectsProvider.Character.gameObject.SetActive(false);
            _objectsProvider.Marker.gameObject.SetActive(false);
            _objectsProvider.Hud.gameObject.SetActive(false);
            _objectsProvider.DirectionArrow.gameObject.SetActive(false);
            UnityEngine.Object.Destroy(_objectsProvider.CurrentRoom.gameObject);
        }

        private void EnterNextState<TNext>()
            where TNext : State
            => _context.Enter<TNext>();
    }
}