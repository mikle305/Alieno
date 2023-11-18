using GamePlay.Characteristics;
using Services;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class ForwardShotComponent : AbilityComponent<ForwardShotData, ForwardShotLevelData>
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
            => Shot();

        private void Shot()
        {
            GameObject prefab = _attack.ProjectilePrefab;
            float speed = _attack.ProjectileSpeed.GetValue();
            float damage = _attack.ProjectileDamage.GetValue();
            Vector3 position = _attack.ForwardShotSpawn.position;
            Vector3 direction = _transform.forward;

            for (int i = 0; i < CurrentLevel.ShotsCount; i++)
            {
                _objectsFactory.CreateProjectile(prefab, position, direction, speed, damage);
                position = new Vector3(position.x - 0.5f, position.y, position.z);
            }
        }
    }
}