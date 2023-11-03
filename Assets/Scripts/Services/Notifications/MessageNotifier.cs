using System;
using System.Collections.Generic;
using Additional.Game;

namespace Services.Notifications
{
    public class MessageNotifier : MonoSingleton<MessageNotifier>
    {
        private Dictionary<ErrorId, string> _internalErrors;
        private Dictionary<MessageId, string> _internalMessages;

        public event Action<string> NotificationHappened;


        protected override void Awake()
        {
            base.Awake();

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
                { MessageId.PasswordResetRequested, "Password reset link was sent on your email" }
            };
        }

        public void NotifyMessage(MessageId messageId)
        {
            string message = _internalMessages[messageId];
            NotificationHappened?.Invoke(message);
        }

        public void NotifyError(ErrorId errorId)
        {
            string message = _internalErrors[errorId];
            NotificationHappened?.Invoke(message);
        }

        public void NotifyError(string errorMessage)
            => NotificationHappened?.Invoke(errorMessage);
    }
}