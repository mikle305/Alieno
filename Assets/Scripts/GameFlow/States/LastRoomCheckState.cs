using System;
using Additional.Constants;
using GamePlay.Abilities;
using SaveData;
using Services;
using Services.Save;

namespace GameFlow.States
{
    public class LastRoomCheckState : State
    {
        private readonly GameStateMachine _context;
        private readonly ObjectsProvider _objectsProvider;
        private readonly SaveService _saveService;


        public LastRoomCheckState(GameStateMachine context, ObjectsProvider objectsProvider, SaveService saveService)
        {
            _context = context;
            _objectsProvider = objectsProvider;
            _saveService = saveService;
        }

        public override void Enter()
        {
            if (IsLastRoom())
            {
                SetNextLevelProgress();
                EnterRoomExitWaiting();
            }
            else
            {
                EnterAbilityGeneration();
            }
        }

        private void SetNextLevelProgress()
        {
            PlayerData playerData = _saveService.Progress.PlayerData;
            playerData.Room = DefaultPlayerProgress.Room;
            playerData.Level++;
            playerData.CurrentHealth = DefaultPlayerProgress.Health;
            playerData.GeneratedAbilities = Array.Empty<AbilityId>();
            playerData.CurrentAbilities = DefaultPlayerProgress.GetAbilities();

            _saveService.Save();
        }

        private void EnterAbilityGeneration()
            => _context.Enter<AbilitiesGenerationState>();

        private void EnterRoomExitWaiting()
            => _context.Enter<RoomExitWaitingState, MainMenuState>();

        private bool IsLastRoom()
        {
            int currentRoom = _saveService.Progress.PlayerData.Room;
            return currentRoom == _objectsProvider.Rooms.Length + 1;
        }
    }
}