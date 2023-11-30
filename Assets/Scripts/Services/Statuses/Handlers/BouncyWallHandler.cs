using GamePlay.Statuses;

namespace Services.Statuses
{
    public class BouncyWallHandler : StatusHandler<BouncyWallStatus>
    {
        protected override bool OnHandle(DamageData damageData, BouncyWallStatus status) 
            => true;
    }
}