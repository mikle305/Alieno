using System;
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
        [SerializeField] private Button _confirmButton;

        private Dictionary<ErrorId, string> _internalErrors;
        private Dictionary<MessageId, string> _internalMessages;
        private NotificationService _notificationService;
        private Action _actionOnConfirm;


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
            _notificationService.MessageReceived += ShowMessage;
            _notificationService.InternalErrorReceived += ShowError;
            _notificationService.ExternalErrorReceived += ShowError;
            _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }

        private void UnbindEvents()
        {
            _notificationService.MessageReceived -= ShowMessage;
            _notificationService.InternalErrorReceived -= ShowError;
            _notificationService.ExternalErrorReceived -= ShowError;
            _confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
        }

        private void ShowMessage(MessageId messageId, Action onConfirm)
            => ShowPopup(_internalMessages[messageId], onConfirm);

        private void ShowError(ErrorId errorId, Action onConfirm)
            => ShowPopup(_internalErrors[errorId], onConfirm);

        private void ShowError(string error, Action onConfirm)
            => ShowPopup(error, onConfirm);

        private void ShowPopup(string message, Action onConfirm)
        {
            _actionOnConfirm = onConfirm;
            _text.text = message;
            _popupWindow.Toggle(ToggleMode.Open);
        }

        private void HidePopup()
        {
            _actionOnConfirm = null;
            _text.text = string.Empty;
        }

        private void OnConfirmButtonClicked()
        {
            _actionOnConfirm?.Invoke();
            HidePopup();
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
                { MessageId.ProgressReset, "Are you sure you want to reset your progress?" },
            };
        }
    }
}