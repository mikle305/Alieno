using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Characteristics;
using GamePlay.Statuses;

namespace Services.Statuses
{
    public class ElementHandler<TStatus> : StatusHandler<TStatus>
        where TStatus : ElementStatus
    {
        private readonly Dictionary<HealthData, DamageDataWithElement<TStatus>> _currentReceivers = new();


        protected override bool OnHandle(DamageData damageData, TStatus status)
        {
            SetElementStatusOnReceiver(damageData, status).Forget();
            return true;
        }

        private async UniTask SetElementStatusOnReceiver(DamageData damageData, TStatus status)
        {
            HealthData receiver = damageData.Receiver;
            if (receiver == null || receiver.Current == 0)
                return;

            if (NoStatusOnReceiver(receiver))
            {
                _currentReceivers[receiver] = CreateReceiversElementMap(damageData, status);
                await StartNewLoop(receiver);
            }
            else
            {
                _currentReceivers[receiver] = CreateReceiversElementMap(damageData, status);
            }
        }

        private async UniTask StartNewLoop(HealthData receiver)
        {
            var tokenSource = new CancellationTokenSource();
            receiver.ZeroReached += tokenSource.Cancel;

            while (!tokenSource.IsCancellationRequested)
            {
                if (NoStatusOnReceiver(receiver) || _currentReceivers[receiver].Status.CountLeft == 0)
                    break;

                TStatus status = _currentReceivers[receiver].Status;
                DamageData damageData = _currentReceivers[receiver].DamageData;


                await UniTask.WaitForSeconds(status.Rate, cancellationToken: tokenSource.Token);

                status.CountLeft--;
                float damage = damageData.MainDamage * status.DamageCoefficient;
                receiver.Decrease(damage);
            }

            _currentReceivers[receiver] = null;
            receiver.ZeroReached -= tokenSource.Cancel;
        }

        private bool NoStatusOnReceiver(HealthData receiver)
            => !_currentReceivers.ContainsKey(receiver) || _currentReceivers[receiver] == null;

        private DamageDataWithElement<TStatus> CreateReceiversElementMap(DamageData damageData, TStatus status)
            => new()
            {
                DamageData = damageData,
                Status = status,
            };
    }
}