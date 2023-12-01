using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Additional.Game
{
    public class GameObjectPool : ObjectPool<GameObject>
    {
        public GameObjectPool(GameObject prefab, int startCount, Transform parent = null, Action<GameObject, ObjectPool<GameObject>> onCreate = null)
            : base(() => CreateObject(prefab, parent), startCount, onCreate)
        {
        }

        public override void Release(GameObject obj)
        {
            obj.SetActive(false);
            base.Release(obj);
        }

        public override GameObject Take()
        {
            GameObject obj = base.Take();
            obj.SetActive(true);
            return obj;
        }

        private static GameObject CreateObject(GameObject prefab, Transform parent)
        {
            prefab.SetActive(false);
            GameObject obj = Object.Instantiate(prefab, parent);
            return obj;
        }
    }

    public class GameObjectPool<T> : ObjectPool<T>
        where T : Component
    {
        public GameObjectPool(T prefab, int startCount, Transform parent = null, Action<T, ObjectPool<T>> onCreate = null)
            : base(() => CreateObject(prefab, parent), startCount, onCreate)
        {
        }


        public override void Release(T component)
        {
            component.gameObject.SetActive(false);
            base.Release(component);
        }

        public override T Take()
        {
            T component = base.Take();
            component.gameObject.SetActive(true);
            return component;
        }

        private static T CreateObject(T prefab, Transform parent)
        {
            prefab.gameObject.SetActive(false);
            T obj = Object.Instantiate(prefab, parent);
            return obj;
        }
    }
}