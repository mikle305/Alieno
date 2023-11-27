using GamePlay.Abilities;
using Services;
using UnityEngine;

namespace UI.GamePlay
{
    public class AbilitySelectionView : MonoBehaviour
    {
        [SerializeField] private AbilityButton[] _buttons;
        
        private AbilitySelectionService _abilitySelectionService;
        
        
        private void Start()
        {
            _abilitySelectionService = AbilitySelectionService.Instance;
            _abilitySelectionService.AbilitiesGenerated += OnAbilitiesGenerated;
        }

        private void OnAbilitiesGenerated(AbilityId[] abilities)
        {
            for (var i = 0; i < abilities.Length; i++)
            {
                _buttons[i].AbilityId = abilities[i];
                _buttons[i].Text.text = abilities[i].ToString();
                _buttons[i].ButtonClicked += OnButtonClicked;
                _buttons[i].gameObject.SetActive(true);
            }
        }

        private void OnButtonClicked(AbilityButton abilityButton)
        {
            foreach (AbilityButton button in _buttons) 
                button.ButtonClicked -= OnButtonClicked;
            
            _abilitySelectionService.SetSelectedAbility(abilityButton.AbilityId);
        }
    }
}