using GamePlay.Statuses;

namespace Services.Statuses
{
    public class ObstaclePenetrationHandler : StatusHandler<ObstaclePenetrationStatus>
    {
        protected override bool OnHandle(DamageData damageData, ObstaclePenetrationStatus status)
        {
            if (damageData.Receiver != null) 
                return true;

            if (status.CountLeft == 0)
                return true;

            status.CountLeft--;
            return false;
        }
    }
}