using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Characteristics;
using GamePlay.Damage;

namespace Services.Damage
{
    public class PoisonHandler : StatusHandler<PoisonStatus>
    {
        protected override bool OnHandle(DamageData damageData, PoisonStatus status)
        {
            StartDamageLoop(damageData, status).Forget();
            return true;
        }

        private async UniTask StartDamageLoop(DamageData damageData, PoisonStatus status)
        {
            HealthData receiverHealth = damageData.Receiver;
            if (receiverHealth == null)
                return;

            var cts = new CancellationTokenSource();
            receiverHealth.ZeroReached += cts.Cancel;
            while (!cts.IsCancellationRequested)
            {
                float damage = damageData.MainDamage * status.DamagePercents / 100; 
                receiverHealth.Decrease(damage);
                await UniTask.WaitForSeconds(status.Rate, cancellationToken: cts.Token);
            }

            receiverHealth.ZeroReached -= cts.Cancel;
        }
    }
}