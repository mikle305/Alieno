using System;
using System.Collections.Generic;
using System.Linq;
using Additional.Game;
using UnityEngine;

namespace Services.ObjectPool
{
    public class PoolsProvider<TId>
    {
        private readonly Transform _poolsParent;
        private readonly Func<TId, GameObject> _prefabGetter;
        private readonly Dictionary<TId, IObjectPool<GameObject>> _pools;


        public PoolsProvider(Transform poolsParent, Func<TId, GameObject> prefabGetter, PoolEntry<TId>[] defaultPools)
        {
            _prefabGetter = prefabGetter;
            _poolsParent = poolsParent;
            _pools = defaultPools.ToDictionary(entry => entry.Id, CreatePool);
        }
        
        public GameObject TakeItem(TId id)
            => _pools.TryGetValue(id, out IObjectPool<GameObject> pool)
                ? pool.Take()
                : throw new InvalidOperationException("Pool is not initialized");

        private IObjectPool<GameObject> CreatePool(PoolEntry<TId> entry)
        {
            Transform parent = new GameObject($"{entry.Id.ToString()}Pool").transform;
            parent.parent = _poolsParent;
            GameObject prefab = _prefabGetter?.Invoke(entry.Id);
            int startCount = entry.StartCount;
            
            return new GameObjectPool(prefab, startCount, parent, InitPoolable);
        }

        private static void InitPoolable(GameObject obj, IObjectPool<GameObject> pool) 
            => obj.AddComponent<Poolable>().Init(pool);
    }
}