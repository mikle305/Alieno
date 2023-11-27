using System;
using Additional.Constants;
using GameFlow.Context;
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


        public LastRoomCheckState(GameStateMachine context)
        {
            _context = context;
            _objectsProvider = ObjectsProvider.Instance;
            _saveService = SaveService.Instance;
        }

        public override void Enter()
        {
            if (IsLastRoom())
            {
                SetNextLevelProgress();
                EnterMainMenu();
            }
            else
            {
                EnterAbilitySelection();
            }
        }

        private void SetNextLevelProgress()
        {
            PlayerData playerData = _saveService.Progress.PlayerData;
            playerData.Room = DefaultPlayerProgress.Room;
            playerData.Level++;
            playerData.CurrentHealth = DefaultPlayerProgress.Health;
            playerData.SelectionAbilities = Array.Empty<AbilityId>();
            playerData.CurrentAbilities = DefaultPlayerProgress.GetAbilities();

            _saveService.Save();
        }

        private void EnterAbilitySelection()
            => _context.Enter<AbilitySelectionState>();

        private void EnterMainMenu()
            => _context.Enter<MainMenuState>();

        private bool IsLastRoom()
        {
            int currentRoom = _saveService.Progress.PlayerData.Room;
            return currentRoom == _objectsProvider.Rooms.Length;
        }
    }
}