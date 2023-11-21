using GamePlay.Characteristics;
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
            GameObject prefab = _projectileAttackData.Prefab;
            float speed = _projectileAttackData.MoveSpeed.GetValue();
            float damage = _projectileAttackData.Damage.GetValue();
            Vector3[] spawnPoints = _projectileAttackData.GetSpawnPoints(AbilityId, CurrentLevelId);
            Vector3 direction = _transform.forward;

            for (var i = 0; i < CurrentLevel.ShotsCount; i++)
                _objectsFactory.CreateProjectile(prefab, spawnPoints[i], direction, speed, damage);
        }
    }
}