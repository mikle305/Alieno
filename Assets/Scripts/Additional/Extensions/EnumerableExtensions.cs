using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Additional.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TItem> ForEach<TItem>(this IEnumerable<TItem> source, Action<TItem> action)
        {
            IEnumerable<TItem> convertedSource = source as TItem[] ?? source.ToArray();
            foreach (TItem item in convertedSource)
                action.Invoke(item);
            
            return convertedSource;
        }
        
        public static void DebugForEach<TItem>(this IEnumerable<TItem> source, Func<TItem, object> action)
        {
            foreach (TItem item in source)
                Debug.Log(action.Invoke(item));
        }
    }
}