using System;
using System.Collections.Generic;

namespace Additional.Game
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private readonly Queue<T> _objects;
        private readonly Func<T> _creator;
        private readonly Action<T, ObjectPool<T>> _onCreate;
        

        public ObjectPool(Func<T> creator, int startCount, Action<T, ObjectPool<T>> onCreate = null)
        {
            _creator = creator;
            _onCreate = onCreate;
            _objects = CreateCollection(startCount);
            AddToCollection(startCount);
        }

        public virtual T Take()
        {
            return _objects.TryDequeue(out T obj) 
                ? obj 
                : CreateObject();
        }

        public virtual void Release(T obj) 
            => _objects.Enqueue(obj);

        public T[] TakeMany(int count)
        {
            var objects = new T[count];
            for (var i = 0; i < count; i++) 
                objects[i] = Take();

            return objects;
        }

        private static Queue<T> CreateCollection(int startCount)
        {
            int startLength = startCount < 16
                ? 16
                : startCount;

            return new Queue<T>(startLength);
        }

        private void AddToCollection(int count)
        {
            for (var i = 0; i < count; i++)
                Release(CreateObject());
        }

        private T CreateObject()
        {
            T obj = _creator.Invoke();
            _onCreate.Invoke(obj, this);
            return obj;
        }
    }
}