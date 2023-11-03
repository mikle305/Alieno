using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Animations
{
    public class IntTextTween : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _countTextOnVictory;
        [SerializeField, Range(0, 5)] private float _animTime = 1;

        private int _lastValue;
        private Tween _tween;


        public void ChangeCountText(int value)
        {
            _countTextOnVictory.text = value.ToString();
            _tween?.Kill();
            _tween = DOTween
                .To(() => _lastValue, c => _lastValue = c, value, _animTime)
                .OnUpdate(() => _countText.text = _lastValue.ToString());
        }
    }
}