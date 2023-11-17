using GamePlay.StatsSystem;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float _defaultUseRate;
        [SerializeField] private float _defaultProjectileDamage;
        [SerializeField] private float _defaultProjectileSpeed;
        
        [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
        [field: SerializeField] public Transform ForwardShotSpawn { get; private set; }
        
        public ModifiableStat ProjectileDamage { get; private set; }
        public ModifiableStat ProjectileSpeed { get; private set; }
        public ModifiableStat UseRate { get; private set; }


        private void Awake()
        {
            UseRate = new ModifiableStat(_defaultUseRate);
            ProjectileDamage = new ModifiableStat(_defaultProjectileDamage);
            ProjectileSpeed = new ModifiableStat(_defaultProjectileSpeed);
        }
    }
}