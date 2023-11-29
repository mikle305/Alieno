using Additional.Game;
using GamePlay.Damage;

namespace Services.Damage
{
    public class DamageService : MonoSingleton<DamageService>
    {
        private StatusHandler[] _handlers;
        private RandomService _randomService;


        protected void Start()
        {
            _randomService = RandomService.Instance;
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
                new MainDamageHandler(_randomService),
                new PoisonHandler(),
                new FlameHandler(),
                new ObstaclePenetrationHandler(),
                new DisposeHandler(),
            };
        }
    }
}