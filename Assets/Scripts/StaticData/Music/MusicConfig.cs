﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace StaticData.Music
{
    [CreateAssetMenu(menuName = "StaticData/Music Config", fileName = "MusicConfig")]
    public class MusicConfig : ScriptableObject
    {
        [SerializeField] private List<Music> _musicCollection;
        [field: SerializeField] public AudioMixer AudioMixer { get; private set; }

        public Music GetMusic(MusicId id)
            => _musicCollection
                .FirstOrDefault(m => m.Id == id);
    }
}