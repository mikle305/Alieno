using GamePlay.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Characteristics
{
    public class DashData : MonoBehaviour
    {
        [FormerlySerializedAs("_defaultUseRate")] [SerializeField] private float _defaultRate = 2;
        [SerializeField] private float _defaultDistance = 5;
        [SerializeField] private float _defaultSpeed = 10;
        
        public ModifiableStat Rate { get; private set; }
        public ModifiableStat Distance { get; private set; }
        public ModifiableStat Speed { get; private set; }


        private void Awake()
        {
            Rate = new ModifiableStat(_defaultRate);
            Distance = new ModifiableStat(_defaultDistance);
            Speed = new ModifiableStat(_defaultSpeed);
        }
    }
}