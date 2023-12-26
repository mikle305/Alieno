using GameFlow.States;
using Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameFlow.Context
{
    public class BootLifetimeScope : LifetimeScope
    {
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private AudioSource _musicSource;
        
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterEntryPoint(builder);
        }

        private void RegisterEntryPoint(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(r =>
            {
                BindObjectsToProvider(r);
                EnterBootState(r);
            });
        }

        private void BindObjectsToProvider(IObjectResolver resolver)
        {
            var objectsProvider = resolver.Resolve<ObjectsProvider>();
            objectsProvider.UiCamera = _uiCamera;
            objectsProvider.MusicSource = _musicSource;
        }

        private void EnterBootState(IObjectResolver resolver)
            => resolver
                .Resolve<GameStateMachine>()
                .Enter<BootState>();
    }
}