using GamePlay.Statuses;

namespace Services.Statuses
{
    public class HealthAbsorptionHandler : StatusHandler<HealthAbsorptionStatus>
    {
        protected override bool OnHandle(DamageData damageData, HealthAbsorptionStatus status)
        {
            if (damageData.Receiver == null || damageData.Receiver.Current != 0)
                return true;
                
            float heal = CalculateHeal(damageData, status);
            damageData.Sender.Increase(heal);
            return true;
        }

        private static float CalculateHeal(DamageData damageData, HealthAbsorptionStatus status) 
            => damageData.Sender.Max * status.MaxHealthCoefficient;
    }
}