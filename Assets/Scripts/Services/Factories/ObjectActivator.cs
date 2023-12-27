using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Services.Factories
{
    public class ObjectActivator
    {
        private readonly IObjectResolver _monoResolver;

        
        public ObjectActivator(IObjectResolver monoResolver)
        {
            _monoResolver = monoResolver;
        }

        public T CreateSafe<T>(Type objectType, bool tryManually = false) where T : class 
            => Create(objectType, tryManually) as T;
        
        public T CreateUnsafe<T>(Type objectType, bool tryManually = false) where T : class 
            => (T) Create(objectType, tryManually);

        public T Create<T>()
            => _monoResolver.Resolve<T>();

        public object Create(Type objectType, bool tryManually = false)
        {
            object resolved = null;
            try
            {
                resolved = _monoResolver.Resolve(objectType);
            }
            catch
            {
                // ignored
            }

            if (resolved == null && tryManually)
                return Activator.CreateInstance(objectType);

            return resolved;
        }
        
        public T Instantiate<T>(T prefab)
            where T : Component
        {
            T component = Object.Instantiate(prefab);
            _monoResolver.InjectGameObject(component.gameObject);
            return component;
        }

        public T Instantiate<T>(
            T prefab, 
            Transform parent, 
            bool worldPositionStays = false)
            where T : Component
        {
            T component = Object.Instantiate(prefab, parent, worldPositionStays);
            _monoResolver.InjectGameObject(component.gameObject);
            return component;
        }

        public T Instantiate<T>(
            T prefab,
            Vector3 position,
            Quaternion rotation)
            where T : Component
        {
            T component = Object.Instantiate(prefab, position, rotation);
            _monoResolver.InjectGameObject(component.gameObject);
            return component;
        }

        public T Instantiate<T>(
            T prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent)
            where T : Component
        {
            T component = Object.Instantiate(prefab, position, rotation, parent);
            _monoResolver.InjectGameObject(component.gameObject);
            return component;
        }
        
        public GameObject Instantiate(GameObject prefab)
        {
            GameObject obj = Object.Instantiate(prefab);
            _monoResolver.InjectGameObject(obj);
            return obj;
        }

        public GameObject Instantiate(
            GameObject prefab, 
            Transform parent, 
            bool worldPositionStays = false)
        {
            GameObject obj = Object.Instantiate(prefab, parent, worldPositionStays);
            _monoResolver.InjectGameObject(obj);
            return obj;
        }

        public GameObject Instantiate(
            GameObject prefab,
            Vector3 position,
            Quaternion rotation)
        {
            GameObject obj = Object.Instantiate(prefab, position, rotation);
            _monoResolver.InjectGameObject(obj);
            return obj;
        }

        public GameObject Instantiate(
            GameObject prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent)
        {
            GameObject obj = Object.Instantiate(prefab, position, rotation, parent);
            _monoResolver.InjectGameObject(obj);
            return obj;
        }
    }
}