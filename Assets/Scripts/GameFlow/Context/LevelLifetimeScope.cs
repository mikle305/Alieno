using Cinemachine;
using GamePlay.Abilities;
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
            RegisterAbilitiesComponents(builder);
        }

        private void RegisterEntryPoint(IContainerBuilder builder) 
            => builder.RegisterBuildCallback(BindObjectsToProvider);

        private void RegisterLevelServices(IContainerBuilder builder)
        {
            builder.Register<LevelMapService>(Lifetime.Scoped);
            builder.Register<RadarService>(Lifetime.Scoped);
            builder.Register<DamageService>(Lifetime.Scoped);
            builder.Register<StatusesMapper>(Lifetime.Scoped);
            builder.RegisterEntryPoint<TransparentObstaclesService>(Lifetime.Scoped);
        }
        
        private static void RegisterAbilitiesComponents(IContainerBuilder builder)
        {
            builder.Register<ShotComponent<ForwardShotData>>(Lifetime.Transient);
            builder.Register<ShotComponent<BackShotData>>(Lifetime.Transient);
            builder.Register<ShotComponent<SideShotData>>(Lifetime.Transient);
            builder.Register<ShotComponent<DiagonalShotData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<FlameData, FlameLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<PoisonData, PoisonLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<BouncyWallData, BouncyWallLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<HealthAbsorptionData, HealthAbsorptionLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<ObstaclePenetrationData, ObstaclePenetrationLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<RicochetData, RicochetLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<VampirismData, VampirismLevelData>>(Lifetime.Transient);
        }

        private void BindObjectsToProvider(IObjectResolver resolver)
        {
            var objectsProvider = resolver.Resolve<ObjectsProvider>();
            objectsProvider.MainCamera = _mainCamera;
            objectsProvider.VirtualCamera = _virtualCamera;
        }
    }
}