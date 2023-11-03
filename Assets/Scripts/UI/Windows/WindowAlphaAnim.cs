using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Windows
{
    public class WindowAlphaAnim : Window
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField, Range(0, 1)] private float _targetAlpha = 1;
        [SerializeField, Min(0)] private float _duration = 0.3f;

        private Tween _tween;


        protected override void Show(Action onStart = null, Action onDone = null)
        {
            _tween?.Kill();
            _tween = _canvasGroup
                .DOFade(_targetAlpha, _duration)
                .OnStart(() => onStart?.Invoke())
                .OnComplete(() => onDone?.Invoke());
        }

        protected override void Hide(Action onStart = null, Action onDone = null)
        {
            _tween?.Kill();
            _tween = _canvasGroup
                .DOFade(0, _duration)
                .OnStart(() => onStart?.Invoke())
                .OnComplete(() => onDone?.Invoke());
        }
    }
}