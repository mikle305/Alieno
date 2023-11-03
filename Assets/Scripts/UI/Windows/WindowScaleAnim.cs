using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Windows
{
    public class WindowScaleAnim : Window
    {
        [SerializeField, Min(0)] private float _showDuration = 0.3f;
        [SerializeField, Min(0)] private float _hideDuration = 0.3f;

        private Tween _tween;


        protected override void Show(Action onStart = null, Action onDone = null)
        {
            _tween?.Kill();
            _tween = transform
                .DOScale(1, _showDuration)
                .OnStart(() => onStart?.Invoke())
                .OnComplete(() => onDone?.Invoke());
        }

        protected override void Hide(Action onStart = null, Action onDone = null)
        {
            _tween?.Kill();
            _tween = transform
                .DOScale(0, _hideDuration)
                .OnStart(() => onStart?.Invoke())
                .OnComplete(() => onDone?.Invoke());
        }
    }
}