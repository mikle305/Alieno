using System.Collections.Generic;
using Additional.Game;
using Services.Notifications;
using TMPro;
using UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class NotificationPopup : MonoSingleton<NotificationPopup>
    {
        [SerializeField] private Window _popupWindow;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _invisibleBackground;
        [SerializeField] private Button _confirmButton;

        private Dictionary<ErrorId, string> _internalErrors;
        private Dictionary<MessageId, string> _internalMessages;
        private NotificationService _notificationService;

        
        private void Start()
        {
            InitNotificationsCollections();
            BindEvents();
        }

        private void OnDestroy()
        {
            UnbindEvents();
        }

        private void BindEvents()
        {
            _notificationService = NotificationService.Instance;
            _notificationService.MessageReceived += ShowPopup;
            _notificationService.InternalErrorReceived += ShowPopup;
            _notificationService.ExternalErrorReceived += ShowPopup;
            _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }

        private void UnbindEvents()
        {
            _notificationService.MessageReceived -= ShowPopup;
            _notificationService.InternalErrorReceived -= ShowPopup;
            _notificationService.ExternalErrorReceived -= ShowPopup;
            _confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
        }

        private void ShowPopup(MessageId messageId)
            => ShowPopup(_internalMessages[messageId]);

        private void ShowPopup(ErrorId errorId)
            => ShowPopup(_internalErrors[errorId]);

        private void ShowPopup(string message)
        {
            _text.text = message;
            _invisibleBackground.SetActive(true);
            _popupWindow.Toggle(ToggleMode.Open);
        }

        private void HidePopup()
        {
            _text.text = string.Empty;
            _invisibleBackground.SetActive(false);
            _popupWindow.Toggle(ToggleMode.Close);
        }

        private void OnConfirmButtonClicked()
        {
            HidePopup();
            _notificationService.InvokeConfirm();
        }

        private void InitNotificationsCollections()
        {
            _internalErrors = new Dictionary<ErrorId, string>
            {
                { ErrorId.RequestTimeout, "Request timeout error" },
                { ErrorId.Unknown, "Unknown error" },
                { ErrorId.InvalidEmail, "Invalid email form" },
                { ErrorId.InvalidPassword, "Invalid password form\nMin 8 symbols\nLetters must be english" },
                { ErrorId.InvalidUsername, "Invalid username form\nMin 6 symbols\nEnglish and nums only" },
                { ErrorId.OperationNotSupported, "Operation is not supported yet" },
            };

            _internalMessages = new Dictionary<MessageId, string>
            {
                { MessageId.PasswordResetRequested, "Password reset link was sent on your email" },
                { MessageId.NoLevelsMore, "No levels more yet!\nThanks for playing!" },
            };
        }
    }
}