using System;
using Additional.Game;

namespace Services.Notifications
{
    public class NotificationService : MonoSingleton<NotificationService>
    {
        public event Action<ErrorId> InternalErrorReceived;
        public event Action<MessageId> MessageReceived;
        public event Action<string> ExternalErrorReceived;
        public event Action NotificationConfirmed;
        

        public void NotifyMessage(MessageId messageId) 
            => MessageReceived?.Invoke(messageId);

        public void NotifyError(ErrorId errorId) 
            => InternalErrorReceived?.Invoke(errorId);

        public void NotifyError(string errorMessage)
            => ExternalErrorReceived?.Invoke(errorMessage);

        public void InvokeConfirm()
            => NotificationConfirmed?.Invoke();
    }
}