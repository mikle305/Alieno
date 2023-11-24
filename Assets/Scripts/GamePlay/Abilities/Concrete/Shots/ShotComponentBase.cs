using GamePlay.Characteristics;
using Services;
using UnityEngine;

namespace GamePlay.Abilities
{
    public abstract class ShotComponentBase<TShotData> : AbilityComponent<TShotData, ShotLevelData>
        where TShotData : AbilityData<ShotLevelData>
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
            Vector3[] shotsDirections = CurrentLevel.ShotsDirections;
            
            for (var i = 0; i < shotsDirections.Length; i++)
            {
                Vector3 direction = _transform.rotation * shotsDirections[i].normalized;
                SpawnProjectile(spawnPoints[i], direction);
            }
        }

        private void SpawnProjectile(Vector3 spawnPoint, Vector3 direction) 
            => _projectileFactory.Create(_projectileAttackData, spawnPoint, direction);
    }
}