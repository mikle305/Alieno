using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlay
{
    public class CharacteristicHudView : MonoBehaviour, ICharacteristicView
    {
        [SerializeField] private Slider _slider;
        [InfoBox("Optional", TriMessageType.None)]
        [SerializeField] private TextMeshProUGUI _text;
        
        private CharacteristicPresenter _presenter;


        public void Init(CharacteristicPresenter presenter)
            => _presenter = presenter;

        public void SetValue(float current, float max)
        {
            UpdateBar(current, max);
            UpdateText(current, max);
        }

        private void UpdateBar(float current, float max)
        {
            _slider.value = current / max;
        }

        private void UpdateText(float current, float max)
        {
            if (_text == null)
                return;

            _text.text = $"{Mathf.RoundToInt(current)}/{Mathf.RoundToInt(max)}";
        }
    }
}