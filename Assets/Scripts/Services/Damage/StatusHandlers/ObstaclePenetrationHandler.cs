using GamePlay.Characteristics;
using GamePlay.Damage;

namespace Services.Damage
{
    public class ObstaclePenetrationHandler : StatusHandler<ObstaclePenetrationStatus>
    {
        protected override bool OnHandle(DamageData damageData, ObstaclePenetrationStatus status)
        {
            if (damageData.Receiver.TryGetComponent(out HealthData _)) 
                return true;

            if (status.CountLeft == 0)
                return true;
            
            if (status.CountLeft == -1)
                return false;

            status.CountLeft--;
            return false;
        }
    }
}