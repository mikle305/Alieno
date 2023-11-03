using System;
using System.Collections;
using Additional.Game;
using ScreenTransitions;
using UnityEngine;

namespace UI.Loading
{
    public class LoadingCurtain : MonoSingleton<LoadingCurtain>
    {
        [SerializeField] private TransitionScreen _transitionScreen;
        [SerializeField] private LoadingText _loadingText;
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _showingDuration;

        private bool _transitionInProcess;
        private bool _transitionAlreadyShown = true;
        
        
        public void Enable(Action onCurtainShown = null)
            => StartCoroutine(LoadingCoroutine(true, onCurtainShown));

        public void Disable(Action onTransitionEnded = null) 
            => StartCoroutine(LoadingCoroutine(false, onTransitionEnded));

        private IEnumerator LoadingCoroutine(bool isActive, Action onTransitionEnded)
        {
            yield return new WaitUntil(() => _transitionInProcess == false);
            SetLoadingAnims(isActive, _showingDuration);
            StartTransition(isActive, onTransitionEnded);
        }

        private void StartTransition(bool isActive, Action onTransitionEnded)
        {
            TransitionOperation transitionOperation;
            if (isActive)
            {
                if (_transitionAlreadyShown)
                {
                    onTransitionEnded?.Invoke();
                    return;
                }
                
                transitionOperation = _transitionScreen.Enter();
                transitionOperation.OnCompleted += () => _transitionAlreadyShown = true;
            }
            else
            {
                transitionOperation = _transitionScreen.Exit();
                _transitionAlreadyShown = false;
            }

            _transitionInProcess = true;
            transitionOperation.OnCompleted += () =>
            {
                _transitionInProcess = false;
                onTransitionEnded?.Invoke();
            };
        }

        private void SetLoadingAnims(bool isActive, float duration)
        {
            if (isActive)
            {
                _loadingCircle.Enable(duration);
                _loadingText.Enable(duration);
            }
            else
            {
                _loadingCircle.Disable(duration);
                _loadingText.Disable(duration);
            }
        }
    }
}