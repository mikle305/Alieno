using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}