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

        public GameObject Create(PoolId id, Vector3 position)
        {
            Transform obj = _poolsProvider.GetPool(id).Take().transform;
            obj.position = position;
            obj.rotation = Quaternion.identity;
            return obj.gameObject;
        }
    }
}