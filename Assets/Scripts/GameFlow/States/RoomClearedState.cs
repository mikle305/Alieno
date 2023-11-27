using System;
using Additional.Constants;
using GameFlow.Context;
using GamePlay.Characteristics;
using GamePlay.Other;
using GamePlay.Player;
using SaveData;
using Services;
using Services.Save;
using Object = UnityEngine.Object;

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
            SetCurrentProgress();
            InitPlayerExitDetector(EnterLastRoomCheck);
        }

        public override void Exit()
        {
            DisableRoomDependentObject();
        }

        private void SetCurrentProgress()
        {
            PlayerData playerProgress = _saveService.Progress.PlayerData;
            playerProgress.Room++;
            playerProgress.CurrentHealth = GetCharacterHpToSave();
            _saveService.Save();
        }

        private void InitCurrentRoom()
        {
            int room = _saveService.Progress.PlayerData.Room;
            _currentRoom = _objectsProvider.Rooms[room - 1];
        }

        private void InitPlayerExitDetector(Action onPlayerDetected)
        {
            var detector = _currentRoom.ExitPoint.AddComponent<PlayerCollisionDetector>();
            detector.PlayerEntered += onPlayerDetected;
        }

        private void EnterLastRoomCheck() 
            => _context.Enter<LastRoomCheckState>();

        private void DisableRoomDependentObject()
        {
            _objectsProvider.Character.gameObject.SetActive(false);
            _objectsProvider.Marker.gameObject.SetActive(false);
            _objectsProvider.Hud.gameObject.SetActive(false);
            Object.Destroy(_currentRoom.gameObject);
        }

        private float GetCharacterHpToSave()
        {
            var characterHealth = _objectsProvider.Character.GetComponent<HealthData>();
            return DefaultPlayerProgress.Health * (characterHealth.Current / characterHealth.Max);
        }
    }
}