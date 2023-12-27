using System;
using Additional.Constants;
using Services.Notifications;
using Services.Save;
using VContainer.Unity;

namespace Services
{
    public class MainMenuService : ITickable
    {
        private readonly StaticDataService _staticDataService;
        private readonly SaveService _saveService;
        private readonly NotificationService _notificationService;

        public event Action GameStarted;
        public event Action PlayClicked;


        public MainMenuService(
            StaticDataService staticDataService, 
            SaveService saveService, 
            NotificationService notificationService)
        {
            _staticDataService = staticDataService;
            _saveService = saveService;
            _notificationService = notificationService;
        }

        public void Tick()
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