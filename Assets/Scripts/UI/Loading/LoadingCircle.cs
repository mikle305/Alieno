using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Loading
{
    public class LoadingCircle : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Animation _animation;

        private Tween _showingTextTween;


        public void Enable(float showingDuration)
        {
            ToggleAnimation(true);
            _showingTextTween = DoAlpha(true, showingDuration);
        }

        public void Disable(float showingDuration)
        {
            _showingTextTween = DoAlpha(false, showingDuration);
            if (_showingTextTween != null)
                _showingTextTween.OnKill(() => ToggleAnimation(false));
            else
                ToggleAnimation(false);
        }

        private Tween DoAlpha(bool isActive, float duration)
        {
            _showingTextTween?.Kill();
            int alpha = isActive ? 1 : 0;
            return _image.DOFade(alpha, duration);
        }

        private void ToggleAnimation(bool isActive)
            => _animation.enabled = isActive;
    }
}