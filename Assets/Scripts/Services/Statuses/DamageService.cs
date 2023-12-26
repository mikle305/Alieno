using GamePlay.Statuses;
using UnityEngine;

namespace Services.Statuses
{
    public class DamageService
    {
        private readonly RandomService _randomService;
        private readonly RadarService _radarService;
        private LayerMask _obstacleLayer;
        private StatusHandler[] _handlers;


        public DamageService(RandomService randomService, RadarService radarService, StaticDataService staticDataService)
        {
            _randomService = randomService;
            _radarService = radarService;
            _obstacleLayer = staticDataService.GetGamePlayConfig().ObstacleLayer;
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
                new BouncyWallHandler(_obstacleLayer),
                new DisposeHandler(),
            };
        }
    }
}