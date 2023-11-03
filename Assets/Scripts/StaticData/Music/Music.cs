using System;
using UnityEngine;

namespace StaticData.Music
{
    [Serializable]
    public class Music
    {
        [SerializeField] private MusicId _id;
        [SerializeField] private AudioClip _clip;
        
        public MusicId Id => _id;
        public AudioClip Clip => _clip;
    }
}