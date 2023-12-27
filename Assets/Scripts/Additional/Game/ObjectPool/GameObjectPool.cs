using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Additional.Game
{
    public class GameObjectPool : ObjectPool<GameObject>
    {
        public GameObjectPool(GameObject prefab, int startCount, Transform parent = null, 
            Func<GameObject, Transform, GameObject> factory = null, 
            Action<GameObject, ObjectPool<GameObject>> onCreate = null)
            : base(() => CreateObject(prefab, parent, factory), startCount, onCreate)
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

        private static GameObject CreateObject(GameObject prefab, Transform parent, 
            Func<GameObject, Transform, GameObject> factory)
        {
            prefab.SetActive(false);
            GameObject obj = factory != null ? 
                factory.Invoke(prefab, parent) 
                : Object.Instantiate(prefab, parent);
            
            return obj;
        }
    }
}