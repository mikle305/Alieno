using Additional.Game;
using GamePlay.Other.Ids;
using StaticData;
using StaticData.Prefabs;
using UnityEngine;

namespace Services.ObjectPool
{
    public class ObjectPoolsProvider : MonoSingleton<ObjectPoolsProvider>
    {
        [SerializeField] private PoolEntry<ProjectileId>[] _defaultProjectilePools;
        [SerializeField] private PoolEntry<UiElementId>[] _defaultUiElementPools;

        private PrefabsConfig _prefabsConfig;
        private PoolsProvider<ProjectileId> _projectilePoolsProvider;
        private PoolsProvider<UiElementId> _uiElementPoolsProvider;


        public GameObject TakeProjectile(ProjectileId id)
            => _projectilePoolsProvider.TakeItem(id);

        public GameObject TakeUiElement(UiElementId id)
            => _uiElementPoolsProvider.TakeItem(id);


        private void Start()
        {
            InitPrefabsConfig();
            InitPools();
        }

        private void InitPools()
        {
            _projectilePoolsProvider = CreateProjectilesProvider();
            _uiElementPoolsProvider = CreateUiElementsProvider();
        }


        private PoolsProvider<ProjectileId> CreateProjectilesProvider()
            => new(transform, _prefabsConfig.GetProjectile, _defaultProjectilePools);

        private PoolsProvider<UiElementId> CreateUiElementsProvider()
            => new(transform, _prefabsConfig.GetUiElement, _defaultUiElementPools);

        private void InitPrefabsConfig() 
            => _prefabsConfig = StaticDataService.Instance.GetPrefabsConfig();
    }
}