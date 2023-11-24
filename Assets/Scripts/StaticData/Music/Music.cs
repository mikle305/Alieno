using System;
using UnityEngine;

namespace StaticData.Music
{
    [Serializable]
    public class Music
    {
        [SerializeField] private MusicId _id;
        [SerializeField] private AudioClip _clip;
        [Range(0,1)][SerializeField] private float _basicVolume = 0.1f;
        
        public MusicId Id => _id;
        public AudioClip Clip => _clip;
        public float BasicVolume => _basicVolume ;
    }
}