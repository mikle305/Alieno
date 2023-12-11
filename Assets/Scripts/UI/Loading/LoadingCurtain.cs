using System;
using System.Threading;
using Additional.Game;
using Cysharp.Threading.Tasks;
using TransitionsPlus;
using UnityEngine;

namespace UI.Loading
{
    public class LoadingCurtain : MonoSingleton<LoadingCurtain>
    {
        [SerializeField] private TransitionAnimator _transitionAnimator;
        
        private TransitionAnimator _currentTransition;
        private CancellationTokenSource _tokenSource;


        public void Show(Action onCurtainShown = null)
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            ShowAsync(onCurtainShown, _tokenSource.Token).Forget();
        }

        public void Hide(Action onHidden = null)
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            HideAsync(onHidden, _tokenSource.Token).Forget();
        }

        private async UniTask ShowAsync(Action onShown, CancellationToken token)
        {
            float duration = _transitionAnimator.profile.duration;
            float timer = _transitionAnimator.progress * duration;
            
            await UniTask.WaitUntil(() =>
            {
                if (timer >= duration)
                {
                    _transitionAnimator.SetProgress(1);
                    return true;
                }
                
                _transitionAnimator.SetProgress(timer/duration);
                timer += Time.deltaTime;
                return false;
            }, cancellationToken: token);
            
            onShown?.Invoke();
        }

        private async UniTask HideAsync(Action onHidden, CancellationToken token)
        {
            float duration = _transitionAnimator.profile.duration;
            float timer = _transitionAnimator.progress * duration;
            
            await UniTask.WaitUntil(() =>
            {
                if (timer < 0)
                {
                    _transitionAnimator.SetProgress(0);
                    return true;
                }
                
                _transitionAnimator.SetProgress(timer/duration);
                timer -= Time.deltaTime;
                return false;
            }, cancellationToken: token);
            
            onHidden?.Invoke();
        }
    }
}