using System;
using VContainer;

namespace Services.Factories
{
    public class ObjectFactory
    {
        private readonly IObjectResolver _resolver;

        
        public ObjectFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T CreateSafe<T>(Type objectType, bool tryManually = false) where T : class 
            => Create(objectType, tryManually) as T;
        
        public T CreateUnsafe<T>(Type objectType, bool tryManually = false) where T : class 
            => (T) Create(objectType, tryManually);
        
        public object Create(Type objectType, bool tryManually = false)
        {
            object resolved = _resolver.Resolve(objectType);
            if (resolved == null && tryManually)
                return Activator.CreateInstance(objectType);

            return resolved;
        }
    }
}