using Cinemachine;
using Services;
using Services.Statuses;
using Services.TransparentObstacles;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameFlow.Context
{
    public class LevelLifetimeScope : LifetimeScope
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterEntryPoint(builder);
            RegisterLevelServices(builder);
        }

        private void RegisterEntryPoint(IContainerBuilder builder) 
            => builder.RegisterBuildCallback(BindObjectsToProvider);

        private void RegisterLevelServices(IContainerBuilder builder)
        {
            builder.Register<LevelMapService>(Lifetime.Scoped);
            builder.Register<RadarService>(Lifetime.Scoped);
            builder.Register<DamageService>(Lifetime.Scoped);
            builder.Register<StatusesMapper>(Lifetime.Scoped);
            builder.Register<TransparentObstaclesService>(Lifetime.Scoped);
        }

        private void BindObjectsToProvider(IObjectResolver resolver)
        {
            var objectsProvider = resolver.Resolve<ObjectsProvider>();
            objectsProvider.MainCamera = _mainCamera;
            objectsProvider.VirtualCamera = _virtualCamera;
        }
    }
}