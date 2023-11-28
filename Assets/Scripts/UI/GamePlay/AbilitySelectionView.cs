using GamePlay.Abilities;
using Services;
using UI.Windows;
using UnityEngine;

namespace UI.GamePlay
{
    public class AbilitySelectionView : MonoBehaviour
    {
        [SerializeField] private Window _window;
        [SerializeField] private AbilityButton[] _buttons;
        
        private AbilitySelectionService _abilitySelectionService;
        
        
        private void Awake()
        {
            _abilitySelectionService = AbilitySelectionService.Instance;
            _abilitySelectionService.AbilitiesGenerated += OnAbilitiesGenerated;
        }

        private void OnAbilitiesGenerated(AbilityId[] abilities)
        {
            InitButtons(abilities);
            _window.Toggle(ToggleMode.Open);
        }

        private void OnButtonClicked(AbilityButton abilityButton)
        {
            DisposeButtons();
            _window.Toggle(ToggleMode.Close);
            _abilitySelectionService.SetSelectedAbility(abilityButton.AbilityId);
        }

        private void InitButtons(AbilityId[] abilities)
        {
            for (var i = 0; i < abilities.Length; i++)
            {
                _buttons[i].AbilityId = abilities[i];
                _buttons[i].Text.text = abilities[i].ToString();
                _buttons[i].ButtonClicked += OnButtonClicked;
            }
        }

        private void DisposeButtons()
        {
            foreach (AbilityButton button in _buttons) 
                button.ButtonClicked -= OnButtonClicked;
        }
    }
}