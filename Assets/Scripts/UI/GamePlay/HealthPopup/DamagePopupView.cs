using Additional.Extensions;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.GamePlay
{
    public class DamagePopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private float _fadeDuration = 0.3f;
        [SerializeField] private float _waitingDuration = 0.5f;
        [SerializeField] private float _startScale = 2.5f;
        [SerializeField] private float _endScale = 1.0f;
        [SerializeField] private float _scaleDuration = 0.8f;
        [SerializeField] private float _moveDistance = 0.1f;


        public void Play(float damage, Color color)
        {
            SetText(damage);
            SetColor(color);
            DOTween.Sequence()
                .Join(Fade())
                .Join(Scale())
                .Join(Move())
                .OnComplete(this.Dispose);
        }

        private Tween Move()
        {
            float totalDuration = 2 * _fadeDuration + _waitingDuration;
            return transform.DOLocalMoveY(transform.localPosition.y + _moveDistance, totalDuration);
        }

        private Tween Scale()
        {
            transform.localScale = Vector3.one * _startScale;
            return transform.DOScale(_endScale, _scaleDuration);
        }

        private Tween Fade()
            => DOTween.Sequence()
                .Append(_damageText.DOFade(1, _fadeDuration))
                .AppendInterval(_waitingDuration)
                .Append(_damageText.DOFade(0, _fadeDuration));

        private void SetText(float damage)
            => _damageText.text = $"{(int)damage}";

        private void SetColor(Color color) 
            => _damageText.color = color;
    }
}