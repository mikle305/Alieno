using Additional.Game;
using GamePlay.Characteristics;
using GamePlay.Projectile;
using UnityEngine;

namespace Services
{
    public class ProjectileFactory : MonoSingleton<ProjectileFactory>
    {
        private ObjectPoolsProvider _poolsProvider;

        
        private void Start()
        {
            _poolsProvider = ObjectPoolsProvider.Instance;
        }

        public GameObject Create(ProjectileAttackData attackData, Vector3 spawnPoint, Vector3 direction)
        {
            GameObject projectile = _poolsProvider.TakeProjectile(attackData.ProjectileId);
            if (projectile == null)
                return null;

            Configure(projectile, attackData, spawnPoint, direction);
            return projectile;
        }

        private void Configure(GameObject projectile, ProjectileAttackData attackData, Vector3 spawnPoint, Vector3 direction)
        {
            Transform projectileTransform = projectile.transform;
            projectileTransform.position = spawnPoint;
            projectileTransform.rotation = Quaternion.identity;
            
            float speed = attackData.MoveSpeed.GetValue();
            float damage = attackData.Damage.GetValue();
            projectile.GetComponent<ProjectileMovement>().StartMove(direction, speed);
            projectile.GetComponent<ProjectileDamage>().Init(damage);
        }
    }
}