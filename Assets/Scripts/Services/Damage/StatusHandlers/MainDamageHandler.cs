using GamePlay.Characteristics;
using GamePlay.Damage;

namespace Services.Damage
{
    public class MainDamageHandler : StatusHandler
    {
        public override bool Work(DamageData damageData)
        {
            HealthData receiverHealth = damageData.Receiver;
            if (receiverHealth == null)
                return true;
            
            receiverHealth.Decrease(damageData.MainDamage);
            return true;
        }
    }
}