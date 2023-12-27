using Additional.Constants;
using SaveData;
using Services.Save;
using UnityEngine;
using UnityEngine.Audio;

namespace Services
{
    public class SettingsService
    {
        private readonly SaveService _saveService;
        private readonly AudioMixer _audioMixer;
        
        public SettingsData Settings => _saveService.Progress.SettingsData;

        
        private SettingsService(SaveService saveService, StaticDataService staticDataService)
        {
            _saveService = saveService;
            _audioMixer = staticDataService.GetMusicConfig().AudioMixer;
            _saveService.ProgressLoaded += ApplySettings;
        }

        public void Apply()
        {
            ApplySettings();
            _saveService.Save();
        }

        private void ApplySettings()
        {
            ApplyMixerGroup(GameConstants.VolumeMixerParam, Settings.Volume);
            ApplyMixerGroup(GameConstants.MusicMixerParam, Settings.Music);
            ApplyMixerGroup(GameConstants.SoundMixerParam, Settings.Sfx);
        }
        
        private void ApplyMixerGroup(string mixerParam, float value)
            => _audioMixer.SetFloat(mixerParam, Mathf.Log10(value) * 20);
    }
}