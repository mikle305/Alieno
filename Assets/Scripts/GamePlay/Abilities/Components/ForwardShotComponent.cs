using GamePlay.Characteristics;
using Services;
using StaticData.Abilities;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class ForwardShotComponent : AbilityComponent<ForwardShotData>
    {
        private Transform _transform;
        private Attack _attack;
        private ObjectsFactory _objectsFactory;


        protected override void OnInit()
        {
            _transform = Entity.transform;
            _attack = Entity.GetComponent<Attack>();
            _objectsFactory = ObjectsFactory.Instance;
        }

        public override void OnCall() 
            => CreateBullet();

        private void CreateBullet()
        {
            GameObject prefab = _attack.ProjectilePrefab;
            float speed = _attack.ProjectileSpeed.GetValue();
            float damage = _attack.ProjectileDamage.GetValue();
            Vector3 position = _attack.ForwardShotSpawn.position;
            Vector3 direction = _transform.forward;

            _objectsFactory.CreateProjectile(prefab, position, direction, speed, damage);
        }
    }
}