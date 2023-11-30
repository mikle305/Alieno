using Additional.Game;
using GamePlay.Statuses;

namespace Services.Statuses
{
    public class DamageService : MonoSingleton<DamageService>
    {
        private StatusHandler[] _handlers;
        private RandomService _randomService;
        private RadarService _radarService;


        protected void Start()
        {
            _randomService = RandomService.Instance;
            _radarService = RadarService.Instance;
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
                new VampirismHandler(),
                new HealthAbsorptionHandler(),
                new ElementHandler<FlameStatus>(),
                new ElementHandler<PoisonStatus>(),
                new ObstaclePenetrationHandler(),
                new RicochetHandler(_radarService),
                new BouncyWallHandler(),
                new DisposeHandler(),
            };
        }
    }
}