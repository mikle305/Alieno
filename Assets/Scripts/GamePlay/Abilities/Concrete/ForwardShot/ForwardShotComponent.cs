using GamePlay.Characteristics;
using GamePlay.Projectile;
using Services;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class ForwardShotComponent : AbilityComponent<ForwardShotData, ForwardShotLevelData>
    {
        private Transform _transform;
        private ProjectileAttackData _projectileAttackData;
        private ProjectileFactory _projectileFactory;


        protected override void OnCreate()
        {
            _transform = Entity.transform;
            _projectileAttackData = Entity.GetComponent<ProjectileAttackData>();
            _projectileFactory = ProjectileFactory.Instance;
        }

        public override void OnCall() 
            => Shot();

        private void Shot()
        {
            Vector3[] spawnPoints = _projectileAttackData.GetSpawnPoints(AbilityId, CurrentLevelId);
            Vector3 direction = _transform.forward;
            
            for (var i = 0; i < CurrentLevel.ShotsCount; i++)
                CreateProjectile(spawnPoints[i], direction);
        }

        private void CreateProjectile(Vector3 spawnPoint, Vector3 direction)
        {
            ProjectileId projectileId = _projectileAttackData.ProjectileId;
            float speed = _projectileAttackData.MoveSpeed.GetValue();
            float damage = _projectileAttackData.Damage.GetValue();
            
            GameObject projectile = _projectileFactory.Create(projectileId, spawnPoint);
            projectile.GetComponent<ProjectileMovement>().StartMove(direction, speed);
            projectile.GetComponent<ProjectileDamage>().Init(damage);
        }
    }
}