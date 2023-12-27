using Additional.Constants;
using GameFlow.States;
using GamePlay.Abilities;
using SaveData;
using Services;
using Services.Factories;
using Services.Notifications;
using Services.ObjectPool;
using Services.Save;
using Services.Statuses;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameFlow
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        [SerializeField] private MonoRunner _monoRunnerPrefab;


        protected override void Configure(IContainerBuilder builder)
        {
            RegisterGameStates(builder);
            RegisterFactories(builder);
            RegisterSaveService(builder);
            RegisterInputService(builder);
            RegisterStatusServices(builder);
            RegisterAbilitiesComponents(builder);
            RegisterEmptyMono(builder);
            RegisterOtherServices(builder);
        }

        private void RegisterGameStates(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameStateMachine>().AsSelf();
            builder.Register<BootState>(Lifetime.Singleton);
            builder.Register<MainMenuState>(Lifetime.Singleton);
            builder.Register<SceneLoadingState>(Lifetime.Singleton);
            builder.Register<ProgressRestoreState>(Lifetime.Singleton);
            builder.Register<RoomSelectionState>(Lifetime.Singleton);
            builder.Register<RoomLoadingState>(Lifetime.Singleton);
            builder.Register<RoomClearedState>(Lifetime.Singleton);
            builder.Register<LastRoomCheckState>(Lifetime.Singleton);
            builder.Register<AbilitiesGenerationState>(Lifetime.Singleton);
            builder.Register<AbilitySelectionState>(Lifetime.Singleton);
            builder.Register<RoomExitWaitingState>(Lifetime.Singleton);
            builder.Register<DefeatState>(Lifetime.Singleton);
        }

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<ObjectPoolsProvider>(Lifetime.Singleton);
            builder.Register<ObjectActivator>(Lifetime.Singleton);
            builder.Register<ProjectileFactory>(Lifetime.Singleton);
            builder.Register<HudFactory>(Lifetime.Singleton);
            builder.Register<EnemyFactory>(Lifetime.Singleton);
            builder.Register<GameFactory>(Lifetime.Singleton);
            builder.Register<UiFactory>(Lifetime.Singleton);
        }

        private void RegisterStatusServices(IContainerBuilder builder)
        {
            builder.Register<DamageService>(Lifetime.Singleton);
            builder.Register<StatusHandlersCollection>(Lifetime.Transient);
            builder.Register<StatusesMapper>(Lifetime.Singleton);
        }

        private void RegisterOtherServices(IContainerBuilder builder)
        {
            builder.Register<EnemiesDeathObserver>(Lifetime.Singleton);
            builder.Register<NotificationService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<MainMenuService>().AsSelf();
            builder.RegisterEntryPoint<EndGameMenuService>().AsSelf();
            builder.Register<StaticDataService>(Lifetime.Singleton);
            builder.Register<RandomService>(Lifetime.Singleton);
            builder.Register<MusicService>(Lifetime.Singleton);
            builder.Register<FpsService>(Lifetime.Singleton);
            builder.Register<ObjectsProvider>(Lifetime.Singleton);
            builder.Register<SettingsService>(Lifetime.Singleton);
            builder.Register<AbilitySelectionService>(Lifetime.Singleton);
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<LevelMapService>(Lifetime.Singleton);
            builder.Register<RadarService>(Lifetime.Singleton);
        }

        private void RegisterAbilitiesComponents(IContainerBuilder builder)
        {
            builder.Register<ShotComponent<ForwardShotData>>(Lifetime.Transient);
            builder.Register<ShotComponent<BackShotData>>(Lifetime.Transient);
            builder.Register<ShotComponent<SideShotData>>(Lifetime.Transient);
            builder.Register<ShotComponent<DiagonalShotData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<FlameData, FlameLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<PoisonData, PoisonLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<BouncyWallData, BouncyWallLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<HealthAbsorptionData, HealthAbsorptionLevelData>>(
                Lifetime.Transient);
            builder.Register<StatusAbilityComponent<ObstaclePenetrationData, ObstaclePenetrationLevelData>>(
                Lifetime.Transient);
            builder.Register<StatusAbilityComponent<RicochetData, RicochetLevelData>>(Lifetime.Transient);
            builder.Register<StatusAbilityComponent<VampirismData, VampirismLevelData>>(Lifetime.Transient);
        }

        private void RegisterSaveService(IContainerBuilder builder)
        {
            builder.Register<SaveService>(Lifetime.Singleton);
            builder.RegisterInstance<ISaveStorage<Progress>>(
                new PlayerPrefsStorage<Progress>(GameConstants.PrefsProgressKey));
        }

        private void RegisterInputService(IContainerBuilder builder) 
            => builder.Register<IInputService, InputService>(Lifetime.Singleton);

        private void RegisterEmptyMono(IContainerBuilder builder)
            => builder
                .RegisterComponentInNewPrefab(_monoRunnerPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ICoroutineRunner>();
    }
}