using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace StaticData.Music
{
    [CreateAssetMenu(menuName = "Static Data/Music Config", fileName = "MusicConfig")]
    public class MusicConfig : ScriptableObject
    {
        [SerializeField] private List<Music> _musicCollection;
        [field: SerializeField] public AudioMixer AudioMixer { get; private set; }


        public AudioClip GetMusicClip(MusicId id)
            => _musicCollection
                .FirstOrDefault(m => m.Id == id)
                ?.Clip;
    }
}