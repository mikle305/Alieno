using Additional.Extensions;
using GamePlay.Statuses;

namespace Services.Statuses
{
    public class DisposeHandler : StatusHandler
    {
        public override bool Work(DamageData damageData)
        {
            damageData.Projectile.Dispose();
            return false;
        }
    }
}