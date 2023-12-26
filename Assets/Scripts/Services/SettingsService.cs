using Additional.Constants;
using Additional.Game;
using SaveData;
using Services.Save;
using UnityEngine;
using UnityEngine.Audio;

namespace Services
{
    public class SettingsService : MonoSingleton<SettingsService>
    {
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
            ApplyMixerGroup(AudioConstants.VolumeMixerParam, Settings.Volume);
            ApplyMixerGroup(AudioConstants.MusicMixerParam, Settings.Music);
            ApplyMixerGroup(AudioConstants.SoundMixerParam, Settings.Sfx);
        }
        
        private void ApplyMixerGroup(string mixerParam, float value)
            => _audioMixer.SetFloat(mixerParam, Mathf.Log10(value) * 20);
    }
}