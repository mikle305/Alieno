using Additional.Extensions;

namespace Services
{
    public class DisposeHandler : StatusHandler
    {
        public override bool Handle(DamageData damageData)
        {
            damageData.Projectile.Dispose();
            return false;
        }
    }
}