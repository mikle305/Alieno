using System.Collections.Generic;
using System.Linq;
using Additional.Game;
using UnityEngine;

namespace Services
{
    public class ObjectPoolsProvider : MonoSingleton<ObjectPoolsProvider>
    {
        [SerializeField] private ProjectilePoolEntry[] _defaultProjectilePools;
        
        private Dictionary<ProjectileId, IObjectPool<Poolable>> _projectilePools;
        private StaticDataService _staticDataService;
        private Dictionary<ProjectileId, GameObject> _projectilePrefabs;


        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
            InitProjectilePrefabsMap();
            InitProjectilePools();
        }

        public Poolable TakeProjectile(ProjectileId id) 
            => _projectilePools.TryGetValue(id, out IObjectPool<Poolable> pool) 
                ? pool.Take() 
                : null;

        private void InitProjectilePools() 
            => _projectilePools = _defaultProjectilePools.ToDictionary(entry => entry.Id, CreateProjectilePool);

        private IObjectPool<Poolable> CreateProjectilePool(ProjectilePoolEntry entry)
        {
            Transform parent = new GameObject(entry.Id.ToString()).transform;
            parent.parent = transform;

            var prefab = _projectilePrefabs[entry.Id].AddComponent<Poolable>();
            return new GameObjectPool<Poolable>(prefab, entry.StartCount, parent, InitPoolable);
        }

        private static void InitPoolable(Poolable obj, IObjectPool<Poolable> pool)
            => obj.Init(pool);

        private void InitProjectilePrefabsMap() 
            => _projectilePrefabs = _staticDataService
                .GetPrefabsConfig()
                .Projectiles
                .ToDictionary(x => x.Id, x => x.Prefab);
    }
}