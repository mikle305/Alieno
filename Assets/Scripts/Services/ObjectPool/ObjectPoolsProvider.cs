using System.Collections.Generic;
using System.Linq;
using Additional.Game;
using UnityEngine;

namespace Services
{
    public class ObjectPoolsProvider : MonoSingleton<ObjectPoolsProvider>
    {
        [SerializeField] private PoolEntry[] _poolsEntries;
        
        private Dictionary<ObjectId, IObjectPool<Poolable>> _poolsMap;


        protected override void Awake()
        {
            base.Awake();
            InitPools();
        }

        public IObjectPool<Poolable> GetPool(ObjectId id)
            => _poolsMap[id];

        private void InitPools() 
            => _poolsMap = _poolsEntries.ToDictionary(entry => entry.Id, CreatePool);

        private IObjectPool<Poolable> CreatePool(PoolEntry entry)
        {
            Transform parent = new GameObject(entry.Id.ToString()).transform;
            parent.parent = transform;
            return new GameObjectPool<Poolable>(entry.Prefab, entry.StartCount, parent, InitPoolable);
        }

        private static void InitPoolable(Poolable obj, IObjectPool<Poolable> pool)
            => obj.Init(pool);
    }
}