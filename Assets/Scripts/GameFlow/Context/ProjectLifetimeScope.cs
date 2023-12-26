using GameFlow.States;
using GamePlay.Abilities;
using SaveData;
using Services;
using Services.Factories;
using Services.Notifications;
using Services.ObjectPool;
using Services.Save;
using VContainer;
using VContainer.Unity;

namespace GameFlow.Context
{
    public class ProjectLifetimeScope : LifetimeScope, ICoroutineRunner
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStateMachine(builder);
            RegisterBootStates(builder);
            RegisterFactories(builder);
            RegisterSaveService(builder);
            RegisterInputService(builder);
            RegisterAbilitiesComponents(builder);
            RegisterOtherServices(builder);
        }

        private static void RegisterStateMachine(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameStateMachine>();
            builder.Register<GameStateFactory>(Lifetime.Singleton);
        }

        private static void RegisterBootStates(IContainerBuilder builder)
        {
            builder.Register<BootState>(Lifetime.Transient);
            builder.Register<MainMenuState>(Lifetime.Transient);
            builder.Register<SceneLoadingState>(Lifetime.Transient);
            builder.Register<ProgressRestoreState>(Lifetime.Transient);
            builder.Register<RoomSelectionState>(Lifetime.Transient);
            builder.Register<RoomLoadingState>(Lifetime.Transient);
            builder.Register<RoomClearedState>(Lifetime.Transient);
            builder.Register<LastRoomCheckState>(Lifetime.Transient);
            builder.Register<AbilitiesGenerationState>(Lifetime.Transient);
            builder.Register<AbilitySelectionState>(Lifetime.Transient);
            builder.Register<RoomExitWaitingState>(Lifetime.Transient);
            builder.Register<DefeatState>(Lifetime.Transient);
        }

        private void RegisterOtherServices(IContainerBuilder builder)
        {
            builder.Register<EnemiesDeathObserver>(Lifetime.Singleton);
            builder.Register<NotificationService>(Lifetime.Singleton);
            builder.Register<MainMenuService>(Lifetime.Singleton);
            builder.Register<EndGameMenuService>(Lifetime.Singleton);
            builder.Register<StaticDataService>(Lifetime.Singleton);
            builder.Register<RandomService>(Lifetime.Singleton);
            builder.Register<MusicService>(Lifetime.Singleton);
            builder.Register<FpsService>(Lifetime.Singleton);
            builder.Register<ObjectsProvider>(Lifetime.Singleton);
            builder.Register<SettingsService>(Lifetime.Singleton);
            builder.Register<AbilitySelectionService>(Lifetime.Singleton);
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.RegisterInstance<ICoroutineRunner>(this);
        }

        private static void RegisterAbilitiesComponents(IContainerBuilder builder)
        {
            builder.Register<ShotComponent<ForwardShotData>>(Lifetime.Transient);
            builder.Register<ShotComponent<BackShotData>>(Lifetime.Transient);
            builder.Register<ShotComponent<SideShotData>>(Lifetime.Transient);
            builder.Register<ShotComponent<DiagonalShotData>>(Lifetime.Transient);
        }

        private static void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<ObjectPoolsProvider>(Lifetime.Singleton);
            builder.Register<ObjectFactory>(Lifetime.Singleton);
            builder.Register<ProjectileFactory>(Lifetime.Singleton);
            builder.Register<HudFactory>(Lifetime.Singleton);
            builder.Register<EnemyFactory>(Lifetime.Singleton);
            builder.Register<GameFactory>(Lifetime.Singleton);
            builder.Register<UiFactory>(Lifetime.Singleton);
        }

        private static void RegisterSaveService(IContainerBuilder builder)
        {
            builder.Register<SaveService>(Lifetime.Singleton);
            builder.Register<ISaveStorage<Progress>, PlayerPrefsStorage<Progress>>(Lifetime.Singleton);
        }

        private static void RegisterInputService(IContainerBuilder builder)
        {
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
        }
    }
}