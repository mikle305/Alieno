using GamePlay.Characteristics;
using Services;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class ForwardShotComponent : AbilityComponent<ForwardShotData, ForwardShotLevelData>
    {
        private Transform _transform;
        private ProjectileAttack _projectileAttack;
        private ObjectsFactory _objectsFactory;


        protected override void OnInit()
        {
            _transform = Entity.transform;
            _projectileAttack = Entity.GetComponent<ProjectileAttack>();
            _objectsFactory = ObjectsFactory.Instance;
        }

        public override void OnCall() 
            => Shot();

        private void Shot()
        {
            GameObject prefab = _projectileAttack.Prefab;
            float speed = _projectileAttack.MoveSpeed.GetValue();
            float damage = _projectileAttack.Damage.GetValue();
            Vector3[] spawnPoints = _projectileAttack.GetSpawnPoints(AbilityId, CurrentLevelId);
            Vector3 direction = _transform.forward;

            for (var i = 0; i < CurrentLevel.ShotsCount; i++)
                _objectsFactory.CreateProjectile(prefab, spawnPoints[i], direction, speed, damage);
        }
    }
}