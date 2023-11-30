using GamePlay.Damage;
using GamePlay.Projectile;
using UnityEngine;

namespace Services.Damage
{
    public class RicochetHandler : StatusHandler<RicochetStatus>
    {
        private readonly RadarService _radarService;

        
        public RicochetHandler(RadarService radarService)
        {
            _radarService = radarService;
        }
        
        protected override bool OnHandle(DamageData damageData, RicochetStatus status)
        {
            if (damageData.Receiver == null)
                return true;

            if (status.CountLeft == 0)
                return true;

            status.CountLeft--;
            Transform nextReceiver = GetNextReceiver(damageData);
            if (nextReceiver == null)
                return true;

            SetDirectionToNextReceiver(damageData, nextReceiver);
            return false;
        }

        private static void SetDirectionToNextReceiver(DamageData damageData, Transform nextReceiver)
        {
            Vector3 heading = nextReceiver.position - damageData.Receiver.transform.position;
            heading.y = 0;
            Vector3 direction = heading.normalized;
            damageData.Projectile.GetComponent<ProjectileMovement>().Direction = direction;
        }

        private Transform GetNextReceiver(DamageData damageData) 
            => _radarService.GetClosestToEnemy(damageData.Receiver.transform);
    }
}