using Services.Notifications;
using Services.Save;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Menu
{
    public class ResetProgressButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private SaveService _saveService;
        private NotificationService _notificationService;
        
        
        [Inject]
        public void Construct(SaveService saveService, NotificationService notificationService)
        {
            _saveService = saveService;
            _notificationService = notificationService;
            _button.onClick.AddListener(NotifyProgressReset);
        }
        
        private void NotifyProgressReset()
            => _notificationService.NotifyMessage(MessageId.ProgressReset, ResetProgress);

        private void ResetProgress()
        {
            _saveService.ResetRoomsProgress();
            _saveService.ResetLevelProgress();
            _saveService.Save();
        }
    }
}