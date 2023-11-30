using GamePlay.Damage;

namespace Services.Damage
{
    public class VampirismHandler : StatusHandler<VampirismStatus>
    {
        protected override bool OnHandle(DamageData damageData, VampirismStatus status)
        {
            if (damageData.Receiver == null)
                return true;
                
            float heal = CalculateHeal(damageData, status);
            damageData.Sender.Increase(heal);
            return true;
        }

        private static float CalculateHeal(DamageData damageData, VampirismStatus status) 
            => damageData.MainDamage * status.DamageCoefficient;
    }
}