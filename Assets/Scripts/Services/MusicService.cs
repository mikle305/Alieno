using Additional.Game;
using StaticData.Music;
using UnityEngine;

namespace Services
{
    public class MusicService : MonoSingleton<MusicService>
    {
        [SerializeField] private AudioSource _audioSource;
    
        private MusicConfig _musicConfig;


        private void Start()
        {
            _musicConfig = StaticDataService.Instance.GetMusicConfig();
        }

        public void Play(MusicId musicId)
        {
            Music music = _musicConfig.GetMusic(musicId);   

            _audioSource.clip = music.Clip;
            _audioSource.volume = music.BasicVolume;
            _audioSource.Play();
        }
    }
}
