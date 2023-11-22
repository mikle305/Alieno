using Additional.Game;
using UnityEngine;

namespace Services
{
    public class ObjectsFactory : MonoSingleton<ObjectsFactory>
    {
        private ObjectPoolsProvider _poolsProvider;

        
        private void Start()
        {
            _poolsProvider = ObjectPoolsProvider.Instance;
        }

        public Transform CreateObject(ObjectId id, Vector3 position, Vector3 direction, float speed, float damage)
        {
            Transform projectile = _poolsProvider.GetPool(id).Take().transform;
            projectile.position = position;
            projectile.rotation = Quaternion.identity;
            return projectile;
        }
    }
}