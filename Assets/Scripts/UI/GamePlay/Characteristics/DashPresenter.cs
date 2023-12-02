using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Player;
using UnityEngine;

namespace UI.GamePlay
{
    public class DashPresenter : ICharacteristicPresenter
    {
        private readonly PlayerDash _dash;
        private readonly ICharacteristicView _view;
        private CancellationTokenSource _cts;
        
        
        public DashPresenter(PlayerDash dash, ICharacteristicView view)
        {
            _dash = dash;
            _view = view;
            _dash.CooldownStarted += StartCooldownLoop;
        }
        
        public void Unbind()
        {
            _dash.CooldownStarted -= StartCooldownLoop;
            _cts.Cancel();
        }

        private void StartCooldownLoop(float cooldown)
        {
            _cts = new CancellationTokenSource();
            UpdateCooldownAsync(cooldown, _cts.Token).Forget();
        }

        private async UniTask UpdateCooldownAsync(float cooldown, CancellationToken cancellationToken)
        {
            float timeLeft = 0;
            await UniTask.WaitUntil(() =>
            {
                timeLeft += Time.deltaTime;
                if (timeLeft < cooldown)
                {
                    _view.SetValue(timeLeft, cooldown);
                    return false;
                }

                _view.SetValue(cooldown, cooldown);
                return true;
            }, cancellationToken: cancellationToken);
        }
    }
}