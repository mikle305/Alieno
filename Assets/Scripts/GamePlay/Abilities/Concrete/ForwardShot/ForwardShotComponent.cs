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
        private ObjectsFactory _objectsFactory;


        protected override void OnCreate()
        {
            _transform = Entity.transform;
            _projectileAttackData = Entity.GetComponent<ProjectileAttackData>();
            _objectsFactory = ObjectsFactory.Instance;
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
            ObjectId objectId = _projectileAttackData.ObjectId;
            float speed = _projectileAttackData.MoveSpeed.GetValue();
            float damage = _projectileAttackData.Damage.GetValue();
            
            Transform projectile = _objectsFactory.CreateObject(objectId, spawnPoint, direction, speed, damage);
            projectile.GetComponent<ProjectileMovement>().StartMove(direction, speed);
            projectile.GetComponent<ProjectileDamage>().Init(damage);
        }
    }
}