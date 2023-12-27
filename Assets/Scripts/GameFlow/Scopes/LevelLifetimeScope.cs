using Cinemachine;
using Services;
using Services.TransparentObstacles;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameFlow
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
            builder.RegisterEntryPoint<TransparentObstaclesService>(Lifetime.Scoped).AsSelf();
        }

        private void BindObjectsToProvider(IObjectResolver resolver)
        {
            var objectsProvider = resolver.Resolve<ObjectsProvider>();
            objectsProvider.MainCamera = _mainCamera;
            objectsProvider.VirtualCamera = _virtualCamera;
        }
    }
}