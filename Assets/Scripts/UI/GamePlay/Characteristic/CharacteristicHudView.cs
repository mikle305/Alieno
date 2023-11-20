using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlay
{
    public class CharacteristicHudView : MonoBehaviour, ICharacteristicView
    {
        [SerializeField] private Slider _slider;
        
        private CharacteristicPresenter _presenter;


        public void Init(CharacteristicPresenter presenter)
            => _presenter = presenter;

        public void SetValue(float current, float max) 
            => _slider.value = current / max;
    }
}