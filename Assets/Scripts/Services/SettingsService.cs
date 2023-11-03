using Additional.Game;
using SaveData;
using Services.Save;
using UnityEngine;
using UnityEngine.Audio;

namespace Services
{
    public class SettingsService : MonoSingleton<SettingsService>
    {
        private const string _volumeMixerParameter = "Volume";
        private SaveService _saveService;
        private AudioMixer _audioMixer;
        
        public SettingsData Settings => _saveService.Progress.SettingsData;

        
        private void Start()
        {
            _saveService = SaveService.Instance;
            _audioMixer = StaticDataService.Instance.GetMusicConfig().AudioMixer;
            _saveService.ProgressLoaded += ApplySettings;
        }

        public void Apply()
        {
            ApplySettings();
            _saveService.Save();
        }

        private void ApplySettings()
        {
            SetVolume();
        }
        
        private void SetVolume()
            => _audioMixer.SetFloat(_volumeMixerParameter, Mathf.Log10(Settings.Volume) * 20);
    }
}