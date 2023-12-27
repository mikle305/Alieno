using System;
using Additional.Constants;
using Services.Notifications;
using Services.Save;
using VContainer.Unity;

namespace Services
{
    public class MainMenuService : ITickable
    {
        private StaticDataService _staticDataService;
        private SaveService _saveService;
        private NotificationService _notificationService;

        public event Action GameStarted;
        public event Action PlayClicked;


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