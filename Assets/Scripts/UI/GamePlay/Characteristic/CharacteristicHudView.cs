using System;
using System.Collections.Generic;
using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlay
{
    public class CharacteristicHudView : MonoBehaviour, ICharacteristicView
    {
        [SerializeField] private Slider _slider;
        [SerializeField] [InfoBox("Optional", TriMessageType.None)] 
        private TextMeshProUGUI _text;

        [SerializeField, HideIf(nameof(_text), null)]
        private CharacteristicTextShowType _textShowType = CharacteristicTextShowType.Percents;

        private Dictionary<CharacteristicTextShowType, Func<float, float, string>> _textCreatorMap;
        private CharacteristicPresenter _presenter;


        private void Awake()
        {
            InitTextCreatorMap();
        }

        public void Init(CharacteristicPresenter presenter)
        {
            _presenter = presenter;
        }

        public void SetValue(float current, float max)
        {
            UpdateBar(current, max);
            UpdateText(current, max);
        }

        private void UpdateBar(float current, float max) 
            => _slider.value = current / max;

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