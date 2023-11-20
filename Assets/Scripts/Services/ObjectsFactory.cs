using Additional.Game;
using GamePlay.Projectile;
using UnityEngine;

namespace Services
{
    public class ObjectsFactory : MonoSingleton<ObjectsFactory>
    {
        private ObjectsFactory()
        {
        }

        public GameObject CreateProjectile(GameObject prefab, Vector3 position, Vector3 direction, float speed, float damage)
        {
            GameObject projectile = Instantiate(prefab, position, Quaternion.identity);
            projectile.GetComponent<ProjectileMovement>().StartMove(direction, speed);
            projectile.GetComponent<ProjectileDamage>().Init(damage);
            return projectile;
        }
    }
}