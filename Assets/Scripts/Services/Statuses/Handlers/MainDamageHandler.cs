using GamePlay.Characteristics;
using GamePlay.Statuses;

namespace Services.Statuses
{
    public class MainDamageHandler : StatusHandler
    {
        private static RandomService _randomService;


        public MainDamageHandler(RandomService randomService)
        {
            _randomService = randomService;
        }
        
        public override bool Work(DamageData damageData)
        {
            HealthData receiverHealth = damageData.Receiver;
            if (receiverHealth == null)
                return true;
            
            receiverHealth.Decrease(CalculateDamage(damageData));
            return true;
        }

        private static float CalculateDamage(DamageData damageData)
        {
            if (_randomService.TryChance(damageData.CritChance))
                return damageData.MainDamage * damageData.CritMultiplier;

            return damageData.MainDamage;
        }
    }
}