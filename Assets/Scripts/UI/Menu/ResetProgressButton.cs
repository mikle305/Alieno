using Services.Notifications;
using Services.Save;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class ResetProgressButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private SaveService _saveService;
        private NotificationService _notificationService;


        private void Start()
        {
            _saveService = SaveService.Instance;
            _notificationService = NotificationService.Instance;
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