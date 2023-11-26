using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Characteristics;
using GamePlay.Damage;

namespace Services.Damage
{
    public class FlameHandler : StatusHandler<FlameStatus>
    {
        protected override bool OnHandle(DamageData damageData, FlameStatus status)
        {
            StartDamageLoop(damageData, status).Forget();
            return true;
        }

        private async UniTask StartDamageLoop(DamageData damageData, FlameStatus status)
        {
            HealthData receiverHealth = damageData.Receiver;
            if (receiverHealth == null)
                return;

            var cts = new CancellationTokenSource();
            receiverHealth.ZeroReached += cts.Cancel;
            while (!cts.IsCancellationRequested && status.CountLeft != 0)
            {
                status.CountLeft--;
                float damage = damageData.MainDamage * status.DamagePercents / 100; 
                receiverHealth.Decrease(damage);
                await UniTask.WaitForSeconds(status.Rate, cancellationToken: cts.Token);
            }

            receiverHealth.ZeroReached -= cts.Cancel;
        }
    }
}