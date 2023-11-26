using System;
using Additional.Extensions;
using GamePlay.Characteristics;
using GamePlay.UnitsComponents;
using UnityEngine;

namespace GamePlay.Projectile
{
    public class ProjectileDamage : MonoBehaviour, IDestroy
    {
        private float _damage;
        public event Action Happened;
        
        
        public void Init(float damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out HealthData health)) 
                health.Decrease(_damage);

            Happened?.Invoke();
            this.Dispose();
        }
    }
}