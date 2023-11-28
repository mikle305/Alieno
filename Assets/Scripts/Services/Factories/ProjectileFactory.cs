using Additional.Game;
using GamePlay.Characteristics;
using GamePlay.Projectile;
using Services.ObjectPool;
using UnityEngine;

namespace Services.Factories
{
    public class ProjectileFactory : MonoSingleton<ProjectileFactory>
    {
        private ObjectPoolsProvider _poolsProvider;

        
        private void Start()
        {
            _poolsProvider = ObjectPoolsProvider.Instance;
        }

        public ProjectileDamage Create(ProjectileAttackData attackData, Vector3 spawnPoint, Vector3 direction)
        {
            GameObject projectile = _poolsProvider.TakeProjectile(attackData.ProjectileId);
            if (projectile == null)
                return null;

            Configure(projectile, attackData, spawnPoint, direction);
            return projectile.GetComponent<ProjectileDamage>();
        }

        private static void Configure(GameObject projectile, ProjectileAttackData attackData, Vector3 spawnPoint, Vector3 direction)
        {
            Transform projectileTransform = projectile.transform;
            projectileTransform.position = spawnPoint;
            projectileTransform.rotation = Quaternion.identity;
            
            float speed = attackData.MoveSpeed.GetValue();
            float damage = attackData.Damage.GetValue();
            var sender = attackData.GetComponent<HealthData>();
            projectile.GetComponent<ProjectileDamage>().Init(sender, damage);
            projectile.GetComponent<ProjectileMovement>().StartMove(direction, speed);
            if (projectile.TryGetComponent(out ProjectileLifetime projectileLifetime))
                projectileLifetime.Init();
        }
    }
}