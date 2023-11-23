using UnityEngine;

namespace Additional.Extensions
{
    public static class TransformExtensions
    {
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