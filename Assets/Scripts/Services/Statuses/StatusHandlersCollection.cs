using GamePlay.Statuses;
using UnityEngine;

namespace Services.Statuses
{
    public class StatusHandlersCollection
    {
        private readonly RandomService _randomService;
        private readonly RadarService _radarService;
        private readonly LayerMask _obstacleLayer;
        private StatusHandler[] _handlers;


        public StatusHandlersCollection(
            RandomService randomService, 
            RadarService radarService,
            StaticDataService staticDataService)
        {
            _randomService = randomService;
            _radarService = radarService;
            _obstacleLayer = staticDataService.GetGamePlayConfig().ObstacleLayer;
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
                new BouncyWallHandler(_obstacleLayer),
                new DisposeHandler(),
            };
        }

        public StatusHandler[] Get()
            => _handlers;
    }
}