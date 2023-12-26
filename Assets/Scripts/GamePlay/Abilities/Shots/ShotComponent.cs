using GamePlay.Characteristics;
using GamePlay.Projectile;
using Services.Factories;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class ShotComponent<TShotData> : AbilityComponent<TShotData, ShotLevelData>
        where TShotData : AbilityData<ShotLevelData>
    {
        private readonly ProjectileFactory _projectileFactory;
        private ProjectileAttackData _projectileAttackData;
        private Transform _transform;


        public ShotComponent(ProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }
        
        protected override void OnCreate()
        {
            _transform = Entity.transform;
            _projectileAttackData = Entity.GetComponent<ProjectileAttackData>();
        }

        public override void OnShotCalled() 
            => Shot();
        
        private void Shot()
        {
            Vector3[] spawnPoints = _projectileAttackData.GetSpawnPoints(AbilityId, CurrentLevelId);
            Vector3[] shotsDirections = CurrentLevel.ShotsDirections;
            
            for (var i = 0; i < shotsDirections.Length; i++)
            {
                Vector3 direction = _transform.rotation * shotsDirections[i].normalized;
                ProjectileDamage projectile = SpawnProjectile(spawnPoints[i], direction);
                Entity.EndShot(projectile);
            }
        }

        private ProjectileDamage SpawnProjectile(Vector3 spawnPoint, Vector3 direction) 
            => _projectileFactory.Create(_projectileAttackData, spawnPoint, direction);
    }
}