using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Loading
{
    public class LoadingText : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _loadingGroup;
        [SerializeField] private TextMeshProUGUI _pointsText;
        [SerializeField, Range(0, 2)] private float _duration = 1;
        [SerializeField, Min(0)] private int _minPointsCount;
        [SerializeField, Min(0)] private int _maxPointsCount = 3;

        private int _lastPointsCount;
        private Tween _pointsTween;
        private Tween _showingTextTween;


        private void Start() 
            => Enable(0);

        public void Enable(float showingDuration)
        {
            _pointsTween = StartPointsAnim();
            _showingTextTween = DoAlpha(true, showingDuration);
        }

        public void Disable(float showingDuration)
        {
            _showingTextTween = DoAlpha(false, showingDuration);
            if (_showingTextTween != null)
                _showingTextTween.OnKill(StopPointsAnim);
            else
                StopPointsAnim();
        }

        private Tween DoAlpha(bool isActive, float duration)
        {
            _showingTextTween?.Kill();
            int alpha = isActive ? 1 : 0;
            return _loadingGroup.DOFade(alpha, duration);
        }

        private Tween StartPointsAnim()
        {
            _lastPointsCount = _minPointsCount;
            UpdateText();
            return DOTween
                .To(() => _lastPointsCount, x => _lastPointsCount = x, _maxPointsCount, _duration)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear)
                .OnUpdate(UpdateText);
        }

        private void StopPointsAnim() 
            => _pointsTween?.Kill();

        private void UpdateText() 
            => _pointsText.text = string.Concat(Enumerable.Repeat(".", _lastPointsCount));
    }
}
