using Additional.Game;
using DG.Tweening;
using StaticData.Music;
using UnityEngine;

namespace Services
{
    public class MusicService : MonoSingleton<MusicService>
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _transitionDuration = 0.5f;
    
        private MusicConfig _musicConfig;
        private Sequence _transitionSequence;


        private void Start()
        {
            _musicConfig = StaticDataService.Instance.GetMusicConfig();
        }

        public void Play(MusicId musicId)
        {
            Music music = _musicConfig.GetMusic(musicId);
            music.Clip.LoadAudioData();
            
            _transitionSequence?.Kill();
            _transitionSequence = DOTween.Sequence();
            if (_audioSource.clip != null)
                _transitionSequence.Append(DisappearMusic());

            _transitionSequence.Append(AppearMusic(music));
        }

        private Tween DisappearMusic() 
            => _audioSource.DOFade(0, _transitionDuration);

        private Tween AppearMusic(Music music) 
            => _audioSource
                .DOFade(music.BasicVolume, _transitionDuration)
                .OnStart(() =>
                {
                    _audioSource.clip = music.Clip;
                    _audioSource.Play();
                });
    }
}
