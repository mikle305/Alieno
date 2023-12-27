using Additional.Extensions;
using Coffee.UIEffects;
using DG.Tweening;
using GamePlay.Abilities;
using Services;
using StaticData.UI;
using TMPro;
using UI.Animations;
using UnityEngine;
using VContainer;

namespace UI.GamePlay
{
    public class AbilitySelectionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private UITransitionEffect _window;
        [SerializeField] private AbilityButton[] _buttons;

        [Space(5), Header("Animation Settings")] 
        [SerializeField] private float _windowAnimTime = 0.4f;
        [SerializeField] private float _labelAnimTime = 0.3f;
        [SerializeField] private float _buttonAnimTime = 0.5f;

        private AbilitySelectionService _abilitySelectionService;
        private UiConfig _uiConfig;
        private Tween _tween;

        
        [Inject]
        public void Construct(StaticDataService staticDataService, AbilitySelectionService abilitySelectionService)
        {
            _uiConfig = staticDataService.GetUiConfig();
            _abilitySelectionService = abilitySelectionService;
            _abilitySelectionService.AbilitiesGenerated += OnAbilitiesGenerated;
        }

        private void OnDestroy()
        {
            _abilitySelectionService.AbilitiesGenerated -= OnAbilitiesGenerated;
            _tween?.Kill();
        }

        private void OnAbilitiesGenerated(AbilityId[] abilities)
        {
            _tween?.Kill();
            _tween = DOTween.Sequence()
                .Append(FadeWindow(true))
                .Append(ShowLabel())
                .Append(ShowAbilities(abilities))
                .SetUpdate(true);
        }

        private void OnButtonClicked(AbilityButton clickedButton)
        {
            DisableLabelCharsAnim();
            _tween?.Kill();
            _tween = DOTween.Sequence()
                .PrependInterval(0f)
                .Append(HideAbilities())
                .Join(HideLabel())
                .Append(FadeWindow(false))
                .OnComplete(() => SendSelectedAbility(clickedButton))
                .SetUpdate(true);
        }

        private Tween ShowAbilities(AbilityId[] abilities)
        {
            Sequence buttonsSequence = DOTween.Sequence();
            for (var i = 0; i < abilities.Length; i++)
            {
                AbilityButton abilityButton = _buttons[i];
                AbilityId abilityId = abilities[i];
                buttonsSequence
                    .Append(FadeAbilityIcon(abilityButton, true)
                        .OnStart(() => InitAbilityVisual(abilityButton, abilityId)))
                    .Join(FadeAbilityName(abilityButton, true));
            }

            return buttonsSequence.OnComplete(SubscribeButtons);
        }

        private Tween HideAbilities()
        {
            Sequence buttonsSequence = DOTween.Sequence();
            foreach (AbilityButton button in _buttons)
            {
                button.ButtonClicked -= OnButtonClicked;
                buttonsSequence
                    .Join(FadeAbilityIcon(button, false))
                    .Join(FadeAbilityName(button, false).OnComplete(() => ClearAbilityVisual(button)));
            }

            return buttonsSequence;
        }

        private Tween FadeWindow(bool show)
        {
            float targetFade = show ? 1.0f : 0.0f;
            return DOTween.To(
                () => _window.effectFactor, (x) => _window.effectFactor = x,
                targetFade, _windowAnimTime);
        }

        private Tween ShowLabel()
        {
            var moveCharsAnim = _label.GetComponent<MoveCharsTween>();
            return _label.DOFade(1, _labelAnimTime).OnComplete(moveCharsAnim.Enable);
        }

        private Tween HideLabel()
            => _label.DOFade(0, _labelAnimTime);

        private Tween FadeAbilityName(AbilityButton abilityButton, bool show)
        {
            float targetFade = show ? 1.0f : 0.0f;
            return abilityButton.Text.DOFade(targetFade, _buttonAnimTime);
        }

        private Tween FadeAbilityIcon(AbilityButton abilityButton, bool show)
        {
            float targetFade = show ? 1.0f : 0.0f;
            var iconTransition = abilityButton.Icon.GetComponent<UITransitionEffect>();
            return DOTween.To(() => iconTransition.effectFactor, x => iconTransition.effectFactor = x, targetFade,
                _buttonAnimTime);
        }

        private void InitAbilityVisual(AbilityButton abilityButton, AbilityId abilityId)
        {
            AbilityUiData uiData = _uiConfig.GetAbilityData(abilityId);
            abilityButton.AbilityId = abilityId;
            abilityButton.Text.text = uiData.Name;
            abilityButton.Icon.sprite = uiData.Icon;
        }

        private void ClearAbilityVisual(AbilityButton abilityButton)
        {
            abilityButton.Text.text = string.Empty;
            abilityButton.Icon.sprite = null;
        }

        private void DisableLabelCharsAnim()
        {
            var moveCharsAnim = _label.GetComponent<MoveCharsTween>();
            moveCharsAnim.Disable();
        }

        private void SendSelectedAbility(AbilityButton clickedButton)
            => _abilitySelectionService.SetSelectedAbility(clickedButton.AbilityId);

        private void SubscribeButtons()
            => _buttons.ForEach(b => b.ButtonClicked += OnButtonClicked);
    }
}