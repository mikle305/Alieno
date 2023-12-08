using System;
using Additional.Constants;
using Additional.Game;
using Services.Notifications;
using Services.Save;

namespace Services
{
    public class MainMenuService : MonoSingleton<MainMenuService>
    {
        private StaticDataService _staticDataService;
        private SaveService _saveService;
        private NotificationService _notificationService;

        public event Action GameStarted;
        public event Action PlayClicked;


        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
            _saveService = SaveService.Instance;
            _notificationService = NotificationService.Instance;
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
            => GameStarted?.Invoke();
        
        private bool IsLastLevel() 
            => _staticDataService.GetPrefabsConfig().Levels.Length < _saveService.Progress.PlayerData.Level;
        
        private void ShowLastLevelPopup()
            => _notificationService.NotifyMessage(MessageId.NoLevelsMore);
    }
}