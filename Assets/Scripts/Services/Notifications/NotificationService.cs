using System;

namespace Services.Notifications
{
    public class NotificationService
    {
        public event Action<ErrorId, Action> InternalErrorReceived;
        public event Action<MessageId, Action> MessageReceived;
        public event Action<string, Action> ExternalErrorReceived;
        

        public void NotifyMessage(MessageId messageId, Action onConfirm = null) 
            => MessageReceived?.Invoke(messageId, onConfirm);

        public void NotifyError(ErrorId errorId, Action onConfirm = null)
            => InternalErrorReceived?.Invoke(errorId, onConfirm);

        public void NotifyError(string errorMessage, Action onConfirm = null)
            => ExternalErrorReceived?.Invoke(errorMessage, onConfirm);
    }
}