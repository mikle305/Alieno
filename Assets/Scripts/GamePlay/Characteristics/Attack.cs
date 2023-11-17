using GamePlay.StatsSystem;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float _defaultUseRate;
        [SerializeField] private float _defaultDamage;
        
        public IModifications Damage { get; private set; }
        public IModifications UseRate { get; private set; }


        private void Awake()
        {
            UseRate = new ModifiableStat(_defaultUseRate);
            Damage = new ModifiableStat(_defaultDamage);
        }
    }
}