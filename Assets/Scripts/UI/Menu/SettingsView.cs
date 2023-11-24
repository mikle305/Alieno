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
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;

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
            _sfxSlider.onValueChanged.AddListener(SetSfx);
            _musicSlider.onValueChanged.AddListener(SetMusic);
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
            _sfxSlider.value = _settingsService.Settings.Sfx;
            _musicSlider.value = _settingsService.Settings.Music;
        }
        
        private void SetVolume(float volume)
            => _settingsService.Settings.Volume = volume; 
        
        private void SetMusic(float volume)
            => _settingsService.Settings.Music = volume; 
        
        private void SetSfx(float volume)
            => _settingsService.Settings.Sfx = volume;
    }
}