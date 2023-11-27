using Additional.Extensions;
using GamePlay.Damage;

namespace Services.Damage
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