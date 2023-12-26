using Additional.Constants;
using DG.Tweening;
using StaticData.Music;
using UnityEngine;

namespace Services
{
    public class MusicService
    {
        private readonly MusicConfig _musicConfig;
        private readonly AudioSource _audioSource;
        private Sequence _transitionSequence;


        private MusicService(StaticDataService staticDataService, ObjectsProvider objectsProvider)
        {
            _musicConfig = staticDataService.GetMusicConfig();
            _audioSource = objectsProvider.MusicSource;
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
            => _audioSource.DOFade(0, AudioConstants.MusicTransitionDuration);

        private Tween AppearMusic(Music music) 
            => _audioSource
                .DOFade(music.BasicVolume, AudioConstants.MusicTransitionDuration)
                .OnStart(() =>
                {
                    _audioSource.clip = music.Clip;
                    _audioSource.Play();
                });
    }
}
