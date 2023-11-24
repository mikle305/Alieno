using Additional.Game;
using SaveData;
using Services.Save;
using UnityEngine;
using UnityEngine.Audio;

namespace Services
{
    public class SettingsService : MonoSingleton<SettingsService>
    {
        [SerializeField] private string _volumeMixerParam = "Volume";
        [SerializeField] private string _sfxMixerParam = "SFX";
        [SerializeField] private string _musicMixerParam = "Music";
        
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
            ApplyMixerGroup(_volumeMixerParam, Settings.Volume);
            ApplyMixerGroup(_musicMixerParam, Settings.Music);
            ApplyMixerGroup(_sfxMixerParam, Settings.Sfx);
        }
        
        private void ApplyMixerGroup(string mixerParam, float value)
            => _audioMixer.SetFloat(mixerParam, Mathf.Log10(value) * 20);
    }
}