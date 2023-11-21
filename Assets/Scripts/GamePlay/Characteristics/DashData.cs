using System;
using GamePlay.StatsSystem;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class DashData : MonoBehaviour
    {
        [SerializeField] public float _defaultUseRate = 2;
        [SerializeField] public float _defaultDistance = 5;
        [SerializeField] public float _defaultSpeed = 10;
        
        public ModifiableStat UseRate { get; private set; }
        public ModifiableStat Distance { get; private set; }
        public ModifiableStat Speed { get; private set; }


        private void Awake()
        {
            UseRate = new ModifiableStat(_defaultUseRate);
            Distance = new ModifiableStat(_defaultDistance);
            Speed = new ModifiableStat(_defaultSpeed);
        }
    }
}