using System;
using GamePlay.Other.Ids;
using Services.Factories;
using StaticData.Pools;
using StaticData.Prefabs;
using UnityEngine;
using VContainer;

namespace Services.ObjectPool
{
    public class ObjectPoolsProvider
    {
        private readonly ObjectActivator _objectActivator;
        private readonly PrefabsConfig _prefabsConfig;
        private readonly PoolsConfig _poolsConfig;
        
        private PoolsProvider<ProjectileId> _projectilePoolsProvider;
        private PoolsProvider<UiElementId> _uiElementPoolsProvider;


        public ObjectPoolsProvider(StaticDataService staticDataService, ObjectActivator objectActivator)
        {
            _objectActivator = objectActivator;
            _prefabsConfig = staticDataService.GetPrefabsConfig();
            _poolsConfig = staticDataService.GetPoolsConfig();
        }

        public void InitPools()
        {
            Transform root = CreateRoot();
            _projectilePoolsProvider = CreateProjectilePoolsProvider(root);
            _uiElementPoolsProvider = CreateUiElementProvider(root);
        }

        public GameObject TakeProjectile(ProjectileId id)
            => _projectilePoolsProvider.TakeItem(id);

        public GameObject TakeUiElement(UiElementId id)
            => _uiElementPoolsProvider.TakeItem(id);

        private static Transform CreateRoot()
            => new GameObject("Pools").transform;

        private PoolsProvider<ProjectileId> CreateProjectilePoolsProvider(Transform root)
            => CreatePoolsProvider(root, "Projectiles", _prefabsConfig.GetProjectile, _poolsConfig.ProjectilePools);


        private PoolsProvider<UiElementId> CreateUiElementProvider(Transform root)
            => CreatePoolsProvider(root, "UiElements", _prefabsConfig.GetUiElement, _poolsConfig.UiElementPools);

        private PoolsProvider<TCategory> CreatePoolsProvider<TCategory>(
            Transform root,
            string parentName,
            Func<TCategory, GameObject> prefabGetter,
            PoolEntry<TCategory>[] dataCollection)
        {
            Transform categoryParent = new GameObject(parentName).transform;
            categoryParent.parent = root;
            return new PoolsProvider<TCategory>(categoryParent, prefabGetter, dataCollection, _objectActivator);
        }
    }
}