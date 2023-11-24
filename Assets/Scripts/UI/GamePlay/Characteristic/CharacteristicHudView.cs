using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlay
{
    public class CharacteristicHudView : MonoBehaviour, ICharacteristicView
    {
        [SerializeField] 
        private Slider _slider;
        
        [SerializeField, Min(0)] 
        private float _animDuration;
        
        [SerializeField] [InfoBox("Optional", TriMessageType.None)] 
        private TextMeshProUGUI _text;
        
        [SerializeField, HideIf(nameof(_text), null)]
        private CharacteristicTextShowType _textShowType = CharacteristicTextShowType.Percents;

        
        private Dictionary<CharacteristicTextShowType, Func<float, float, string>> _textCreatorMap;
        private ICharacteristicPresenter _presenter;
        private Tween _sliderTween;


        private void Awake()
        {
            InitTextCreatorMap();
        }

        public void Init(ICharacteristicPresenter presenter)
        {
            _presenter = presenter;
        }

        public void SetValue(float current, float max)
        {
            UpdateBar(current, max);
            UpdateText(current, max);
        }

        private void UpdateBar(float current, float max)
        {
            float coefficient = current / max;
            if (_animDuration != 0)
            {
                _sliderTween?.Kill();
                _sliderTween = _slider.DOValue(coefficient, _animDuration);
            }
            else
            {
                _slider.value = coefficient;
            }
        }

        private void UpdateText(float current, float max)
        {
            if (_text == null)
                return;

            Func<float, float, string> textCreator = _textCreatorMap[_textShowType];
            _text.text = textCreator.Invoke(current, max);
        }

        private void InitTextCreatorMap()
        {
            _textCreatorMap = new Dictionary<CharacteristicTextShowType, Func<float, float, string>>
            {
                {
                    CharacteristicTextShowType.Percents,
                    (current, max) => $"{Mathf.RoundToInt(100 * current / max)}%"
                },
                {
                    CharacteristicTextShowType.NumbersRatio,
                    (current, max) => $"{Mathf.RoundToInt(current)}/{Mathf.RoundToInt(max)}"
                },
            };
        }
    }
}