using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class LoopScaleTween : MonoBehaviour
    {
        [SerializeField] private float _targetScale = 1.1f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _ease = Ease.Linear;
        
        private Tween _tween;


        private void Awake()
        {
            _tween = transform
                .DOScale(_targetScale, _duration)
                .SetEase(_ease)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            _tween?.Kill();
        }
    }
}