using GamePlay.StatsSystem;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class DashData : MonoBehaviour
    {
        [SerializeField] private float _defaultUseRate = 2;
        [SerializeField] private float _defaultDistance = 5;
        [SerializeField] private float _defaultSpeed = 10;
        
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