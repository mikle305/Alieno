using Additional.Game;

namespace Services
{
    public class DamageService : MonoSingleton<DamageService>
    {
        private StatusHandler[] _handlers;


        protected override void Awake()
        {
            base.Awake();
            InitHandlers();
        }

        public void Process(DamageData damageData)
        {
            foreach (StatusHandler handler in _handlers)
            {
                bool toNext = handler.Handle(damageData);
                if (!toNext)
                    return;
            }
        }

        private void InitHandlers()
        {
            _handlers = new StatusHandler[]
            {
                new PoisonHandler(),
                new FlameHandler(),
                new DisposeHandler(),
            };
        }
    }
}