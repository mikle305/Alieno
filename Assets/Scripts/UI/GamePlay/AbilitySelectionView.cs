using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GamePlay.Abilities;
using Services;
using StaticData.UI;
using UI.Windows;
using UnityEngine;

namespace UI.GamePlay
{
    public class AbilitySelectionView : MonoBehaviour
    {
        [SerializeField] private Window _window;
        [SerializeField] private AbilityButton[] _buttons;
        
        private AbilitySelectionService _abilitySelectionService;
        private StaticDataService _staticDataService;


        private void Awake()
        {
            _staticDataService = StaticDataService.Instance;
            _abilitySelectionService = AbilitySelectionService.Instance;
            _abilitySelectionService.AbilitiesGenerated += ShowAbilitiesView;
        }

        private void ShowAbilitiesView(AbilityId[] abilities)
            => ShowAbilitiesViewAsync(abilities).Forget();

        private void OnButtonClicked(AbilityButton clickedButton) 
            => HideAbilitiesViewAsync(clickedButton).Forget();

        private async UniTask ShowAbilitiesViewAsync(AbilityId[] abilities)
        {
            InitButtons(abilities);
            await ToggleWindow(ToggleMode.Open);
        }

        private async UniTask HideAbilitiesViewAsync(AbilityButton clickedButton)
        {
            DisposeButtons();
            await ToggleWindow(ToggleMode.Close);
            SetSelectedAbility(clickedButton);
        }

        private void InitButtons(AbilityId[] abilities)
        {
            UiConfig uiConfig = _staticDataService.GetUiConfig();
            for (var i = 0; i < abilities.Length; i++)
            {
                AbilityUiData uiData = uiConfig.GetAbilityData(abilities[i]);
                _buttons[i].AbilityId = abilities[i];
                _buttons[i].Text.text = uiData.Name;
                _buttons[i].Icon.sprite = uiData.Icon;
                _buttons[i].ButtonClicked += OnButtonClicked;
            }
        }

        private void DisposeButtons()
        {
            foreach (AbilityButton button in _buttons)
            {
                button.ButtonClicked -= OnButtonClicked;
                button.Text.text = string.Empty;
                button.Icon.sprite = null;
            }
        }

        private async UniTask ToggleWindow(ToggleMode mode)
        {
            var windowToggled = false;
            _window.Toggle(mode, onDone: () => windowToggled = true);
            await UniTask.WaitUntil(() => windowToggled);
        }

        private void SetSelectedAbility(AbilityButton clickedButton) 
            => _abilitySelectionService.SetSelectedAbility(clickedButton.AbilityId);
    }
}