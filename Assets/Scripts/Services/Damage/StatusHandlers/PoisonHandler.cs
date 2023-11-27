using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Characteristics;
using GamePlay.Damage;

namespace Services.Damage
{
    public class PoisonHandler : StatusHandler<PoisonStatus>
    {
        private readonly Dictionary<HealthData, CancellationTokenSource> _currentReceivers = new();
            
        
        protected override bool OnHandle(DamageData damageData, PoisonStatus status)
        {
            StartDamageLoop(damageData, status).Forget();
            return true;
        }

        private async UniTask StartDamageLoop(DamageData damageData, PoisonStatus status)
        {
            HealthData receiverHealth = damageData.Receiver;
            if (receiverHealth == null || receiverHealth.Current == 0)
                return;

            if (_currentReceivers.TryGetValue(receiverHealth, out CancellationTokenSource oldTokenSource))
                oldTokenSource.Cancel();

            var newTokenSource = new CancellationTokenSource();
            _currentReceivers[receiverHealth] = newTokenSource;
            receiverHealth.ZeroReached += newTokenSource.Cancel;
            
            while (!newTokenSource.IsCancellationRequested)
            {
                float damage = damageData.MainDamage * status.DamagePercents / 100; 
                receiverHealth.Decrease(damage);
                await UniTask.WaitForSeconds(status.Rate, cancellationToken: newTokenSource.Token);
            }

            receiverHealth.ZeroReached -= newTokenSource.Cancel;
        }
    }
}