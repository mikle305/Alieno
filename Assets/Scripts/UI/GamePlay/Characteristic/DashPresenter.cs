using Cysharp.Threading.Tasks;
using GamePlay.Player;
using UnityEngine;

namespace UI.GamePlay
{
    public class DashPresenter : ICharacteristicPresenter
    {
        private readonly PlayerDash _dash;
        private readonly ICharacteristicView _view;

        
        public DashPresenter(PlayerDash dash, ICharacteristicView view)
        {
            _dash = dash;
            _view = view;
            _dash.CooldownStarted += StartCooldownLoop;
        }

        private void StartCooldownLoop(float cooldown) 
            => UpdateCooldownAsync(cooldown).Forget();

        private async UniTask UpdateCooldownAsync(float cooldown)
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
            });
        }
    }
}