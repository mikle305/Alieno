using GameFlow.Context;
using GamePlay.Other;
using GamePlay.Player;
using Services;
using Services.Save;
using UnityEngine;

namespace GameFlow.States
{
    public class RoomClearedState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;
        private readonly ObjectsProvider _objectsProvider;
        private Room _currentRoom;


        public RoomClearedState(GameStateMachine context)
        {
            _context = context;
            _saveService = SaveService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
        }
        
        public override void Enter()
        {
            InitCurrentRoom();
            InitPlayerExitDetector();
        }

        public override void Exit()
            => DisableRoomDependentObject();

        private void InitCurrentRoom()
        {
            int room = _saveService.Progress.PlayerData.Room;
            _currentRoom = _objectsProvider.Rooms[room];
        }

        private void InitPlayerExitDetector()
        {
            var detector = _currentRoom.ExitPoint.AddComponent<PlayerCollisionDetector>();
            detector.PlayerEntered += EnterRoomSelectionState;
        }

        private void EnterRoomSelectionState() 
            => _context.Enter<RoomSelectionState>();

        private void DisableRoomDependentObject()
        {
            _objectsProvider.Character.gameObject.SetActive(false);
            _objectsProvider.Marker.gameObject.SetActive(false);
            _objectsProvider.Hud.gameObject.SetActive(false);
            Object.Destroy(_currentRoom.gameObject);
        }
    }
}