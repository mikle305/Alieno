using Services;
using UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Window _settingsWindow;
        [SerializeField] private Button _applyButton;
        [SerializeField] private Slider _volumeSlider;

        private SettingsService _settingsService;


        private void Start()
        {
            _settingsService = SettingsService.Instance;
            UpdateView();
            InitViewEvents();
        }

        private void InitViewEvents()
        {
            _volumeSlider.onValueChanged.AddListener(SetVolume);
            _applyButton.onClick.AddListener(ApplySettings);
        }

        private void ApplySettings()
        {
            _settingsService.Apply();
            _settingsWindow.Toggle(ToggleMode.Close);
        }

        private void UpdateView()
        {
            _volumeSlider.value = _settingsService.Settings.Volume;
        }
        
        private void SetVolume(float volume)
            => _settingsService.Settings.Volume = volume;
    }
}