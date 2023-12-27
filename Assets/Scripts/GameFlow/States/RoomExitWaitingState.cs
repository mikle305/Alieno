using System;
using GamePlay.Player;
using Services;
using UnityEngine;

namespace GameFlow.States
{
    public class RoomExitWaitingState : State
    {
        private readonly GameStateMachine _context;
        private readonly ObjectsProvider _objectsProvider;
        private PlayerCollisionDetector _lastPlayerDetector;
        private Action _enterNextState;

        
        public RoomExitWaitingState(GameStateMachine context)
        {
            _context = context;
            _objectsProvider = ObjectsProvider.Instance;
        }

        public override void Enter<TNext>()
        {
            _enterNextState = EnterNextState<TNext>;
            EnableExitDirectionArrow();
            InitPlayerExitDetector();
        }

        public override void Exit()
            => DisposeRoomDependentObjects();

        private void EnableExitDirectionArrow()
        {
            Transform exitPoint = _objectsProvider.CurrentRoom.ExitPoint.transform;
            _objectsProvider.DirectionArrow.SetTarget(exitPoint);
            _objectsProvider.DirectionArrow.gameObject.SetActive(true);
        }

        private void InitPlayerExitDetector()
        {
            _lastPlayerDetector = _objectsProvider.CurrentRoom.ExitPoint.AddComponent<PlayerCollisionDetector>();
            _lastPlayerDetector.PlayerEntered += _enterNextState.Invoke;
        }

        private void DisposeRoomDependentObjects()
        {
            if (_enterNextState != null)
                _lastPlayerDetector.PlayerEntered -= _enterNextState;
            
            _objectsProvider.Character.gameObject.SetActive(false);
            _objectsProvider.Hud.gameObject.SetActive(false);
            _objectsProvider.DirectionArrow.gameObject.SetActive(false);
            UnityEngine.Object.Destroy(_objectsProvider.CurrentRoom.gameObject);
        }

        private void EnterNextState<TNext>()
            where TNext : State
            => _context.Enter<TNext>();
    }
}