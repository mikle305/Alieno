using Services;
using UnityEngine;

namespace Additional.Extensions
{
    public static class MonoExtensions
    {
        public static void Dispose(this MonoBehaviour component)
        {
            if (component.TryGetComponent(out Poolable poolable))
                poolable.Release();
            else
                Object.Destroy(component.gameObject);
        }

        public static void Dispose(this GameObject obj)
        {
            if (obj.TryGetComponent(out Poolable poolable))
                poolable.Release();
            else
                Object.Destroy(obj);
        }
        
        public static T[] GetChildren<T>(this Transform parent)
            where T : Component
        {
            int childCount = parent.childCount;
            var components = new T[childCount];

            for (int i = 0; i < childCount; i++)
            {
                components[i] = parent.GetChild(i).GetComponent<T>();
            }

            return components;
        }
    }
}