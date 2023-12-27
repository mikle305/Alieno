using Additional.Constants;
using GamePlay.Characteristics;
using SaveData;
using Services;
using Services.Save;

namespace GameFlow.States
{
    public class RoomClearedState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;
        private readonly ObjectsProvider _objectsProvider;
        

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
            EnterLastRoomCheck();
        }

        private void SetCurrentProgress()
        {
            PlayerData playerProgress = _saveService.Progress.PlayerData;
            playerProgress.Room++;
            playerProgress.AbilitySelected = false;
            playerProgress.CurrentHealth = GetCharacterHpToSave();
            _saveService.Save();
        }

        private void InitCurrentRoom()
        {
            int room = _saveService.Progress.PlayerData.Room;
            _objectsProvider.CurrentRoom = _objectsProvider.Rooms[room - 1];
        }

        private float GetCharacterHpToSave()
        {
            var characterHealth = _objectsProvider.Character.GetComponent<HealthData>();
            return DefaultPlayerProgress.Health * (characterHealth.Current / characterHealth.Max);
        }

        private void EnterLastRoomCheck() 
            => _context.Enter<LastRoomCheckState>();
    }
}