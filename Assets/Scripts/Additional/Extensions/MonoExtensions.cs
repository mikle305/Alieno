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
    }
}