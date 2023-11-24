using Additional.Game;
using UnityEngine;

namespace Services
{
    public class ProjectileFactory : MonoSingleton<ProjectileFactory>
    {
        private ObjectPoolsProvider _poolsProvider;

        
        private void Start()
        {
            _poolsProvider = ObjectPoolsProvider.Instance;
        }

        public GameObject Create(ProjectileId id, Vector3 position)
        {
            GameObject projectile = _poolsProvider.TakeProjectile(id);
            if (projectile != null)
                return SpawnFromPool(projectile, position);

            return null;
        }

        private GameObject SpawnFromPool(GameObject projectile, Vector3 position)
        {
            Transform projectileTransform = projectile.transform;
            projectileTransform.position = position;
            projectileTransform.rotation = Quaternion.identity;
            return projectile;
        }
    }
}