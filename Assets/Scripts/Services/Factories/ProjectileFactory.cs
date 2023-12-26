using Additional.Game;
using GamePlay.Characteristics;
using GamePlay.Projectile;
using Services.ObjectPool;
using UnityEngine;

namespace Services.Factories
{
    public class ProjectileFactory
    {
        private readonly ObjectPoolsProvider _poolsProvider;


        private ProjectileFactory(ObjectPoolsProvider poolsProvider)
        {
            _poolsProvider = poolsProvider;
        }

        public ProjectileDamage Create(ProjectileAttackData attackData, Vector3 spawnPoint, Vector3 direction)
        {
            GameObject projectile = _poolsProvider.TakeProjectile(attackData.ProjectileId);
            if (projectile == null)
                return null;

            Spawn(projectile, spawnPoint, direction);
            Configure(projectile, attackData, direction);
            return projectile.GetComponent<ProjectileDamage>();
        }

        private static void Configure(GameObject projectile, ProjectileAttackData attackData, Vector3 direction)
        {
            ConfigureDamage(projectile, attackData);
            ConfigureMovement(projectile, direction, attackData);
            InitLifeTime(projectile);
        }

        private static void Spawn(GameObject projectile, Vector3 spawnPoint, Vector3 direction)
        {
            Transform projectileTransform = projectile.transform;
            projectileTransform.position = spawnPoint;
            projectileTransform.rotation = Quaternion.LookRotation(direction);
        }

        private static void ConfigureMovement(GameObject projectile, Vector3 direction, ProjectileAttackData attackData)
        {
            var projectileMovement = projectile.GetComponent<ProjectileMovement>();
            projectileMovement.Direction = direction;
            projectileMovement.Speed = attackData.MoveSpeed.GetValue();
            projectileMovement.IsWorking = true;
        }

        private static void ConfigureDamage(GameObject projectile, ProjectileAttackData attackData)
        {
            var projectileDamage = projectile.GetComponent<ProjectileDamage>();
            projectileDamage.Init(
                sender: attackData.GetComponent<HealthData>(),
                attackData.AttackDamage.GetValue(),
                attackData.CritChance.GetValue(),
                attackData.CritMultiplier.GetValue()
            );
        }

        private static void InitLifeTime(GameObject projectile)
        {
            if (projectile.TryGetComponent(out ProjectileLifetime projectileLifetime))
                projectileLifetime.Init();
        }
    }
}