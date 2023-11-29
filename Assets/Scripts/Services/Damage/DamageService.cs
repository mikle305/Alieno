using Additional.Game;
using GamePlay.Damage;

namespace Services.Damage
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
            foreach (StatusHandler statusHandler in _handlers)
            {
                 bool toNext = statusHandler.Work(damageData);
                 if (!toNext)
                     break;
            }
        }

        private void InitHandlers()
        {
            _handlers = new StatusHandler[]
            {
                new MainDamageHandler(),
                new PoisonHandler(),
                new FlameHandler(),
                new ObstaclePenetrationHandler(),
                new DisposeHandler(),
            };
        }
    }
}