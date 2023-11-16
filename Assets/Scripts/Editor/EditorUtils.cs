using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class EditorUtils
    {
        public static void SaveSerialization(Object obj)
        {
            EditorUtility.SetDirty(obj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static T[] GetSoInstances<T>() where T : ScriptableObject
            => AssetDatabase
                .FindAssets($"t:{typeof(T).Name}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToArray();
    }
}