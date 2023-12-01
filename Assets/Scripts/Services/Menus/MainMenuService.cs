using System;
using Additional.Constants;
using Additional.Game;
using SaveData;
using Services.Notifications;
using Services.Save;

namespace Services
{
    public class MainMenuService : MonoSingleton<MainMenuService>
    {
        private StaticDataService _staticDataService;
        private SaveService _saveService;
        private MessageNotifier _messageNotifier;

        public event Action StartGameInvoked;
        public event Action<PlayerData> ProgressReceived;
        public event Action PlayClicked;


        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
            _saveService = SaveService.Instance;
            _messageNotifier = MessageNotifier.Instance;
        }

        private void Update()
        {
            if (!SimpleInput.GetButtonDown(InputConstants.Play)) 
                return;

            if (IsLastLevel())
            {
                ShowLastLevelPopup();
                return;
            }
                
            PlayClicked?.Invoke();
        }
        
        public void StartGame()
            => StartGameInvoked?.Invoke();

        public void DisplayProgress(PlayerData playerProgress)
            => ProgressReceived?.Invoke(playerProgress);
        
        private bool IsLastLevel() 
            => _staticDataService.GetPrefabsConfig().Levels.Length < _saveService.Progress.PlayerData.Level;
        
        private void ShowLastLevelPopup()
            => _messageNotifier.NotifyMessage(MessageId.NoLevelsMore);
    }
}