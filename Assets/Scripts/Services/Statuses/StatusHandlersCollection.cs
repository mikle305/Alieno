using Additional.Constants;
using GamePlay.Statuses;

namespace Services.Statuses
{
    public class StatusHandlersCollection
    {
        private readonly RandomService _randomService;
        private readonly RadarService _radarService;
        private StatusHandler[] _handlers;


        public StatusHandlersCollection(
            RandomService randomService, 
            RadarService radarService)
        {
            _randomService = randomService;
            _radarService = radarService;
        }

        public void Init()
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
                new BouncyWallHandler(GameConstants.ObstacleLayer),
                new DisposeHandler(),
            };
        }

        public StatusHandler[] Get()
            => _handlers;
    }
}