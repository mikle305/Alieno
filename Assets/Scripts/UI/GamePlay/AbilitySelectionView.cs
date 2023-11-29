using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Abilities;
using Services;
using StaticData.UI;
using TMPro;
using UI.Windows;
using UnityEngine;

namespace UI.GamePlay
{
    public class AbilitySelectionView : MonoBehaviour
    {
        [SerializeField] private Window _window;
        [SerializeField] private AbilityButton[] _buttons;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private string _labelText = "Select ability";
        
        private AbilitySelectionService _abilitySelectionService;
        private StaticDataService _staticDataService;


        private void Awake()
        {
            _staticDataService = StaticDataService.Instance;
            _abilitySelectionService = AbilitySelectionService.Instance;
            _abilitySelectionService.AbilitiesGenerated += OnAbilitiesGenerated;
        }

        private void OnAbilitiesGenerated(AbilityId[] abilities)
            => ShowAbilitiesViewAsync(abilities).Forget();

        private void OnButtonClicked(AbilityButton clickedButton) 
            => HideAbilitiesViewAsync(clickedButton).Forget();

        private async UniTask ShowAbilitiesViewAsync(AbilityId[] abilities)
        {
            await ToggleWindow(ToggleMode.Open);
            InitButtons(abilities);
        }

        private async UniTask HideAbilitiesViewAsync(AbilityButton clickedButton)
        {
            DisposeButtons();
            await ToggleWindow(ToggleMode.Close);
            SetSelectedAbility(clickedButton);
        }

        private void InitButtons(AbilityId[] abilities)
        {
            var text = string.Empty;
            DOTween.To(() => text, (x) => text = x, _labelText, 0.5f).OnUpdate(() => _label.text = text);
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