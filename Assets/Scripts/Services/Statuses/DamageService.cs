using GamePlay.Statuses;

namespace Services.Statuses
{
    public class DamageService
    {
        private readonly StatusHandlersCollection _handlersCollection;


        public DamageService(StatusHandlersCollection handlersCollection)
        {
            _handlersCollection = handlersCollection;
            _handlersCollection.Init();
        }

        public void Process(DamageData damageData)
        {
            foreach (StatusHandler statusHandler in _handlersCollection.Get())
            {
                bool toNext = statusHandler.Work(damageData);
                if (!toNext)
                    break;
            }
        }
    }
}