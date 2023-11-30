using GamePlay.Damage;

namespace Services.Damage
{
    public class BouncyWallHandler : StatusHandler<BouncyWallStatus>
    {
        protected override bool OnHandle(DamageData damageData, BouncyWallStatus status) 
            => true;
    }
}