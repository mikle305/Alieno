using UnityEngine;

namespace GamePlay.Projectile
{
    public class ProjectileDamage : MonoBehaviour
    {
        private float _damage;

        
        public void Init(float damage)
        {
            _damage = damage;
        }
    }
}