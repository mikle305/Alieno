using System;
using System.Collections.Generic;
using System.Linq;
using Additional.Game;
using Random = UnityEngine.Random;

namespace Services
{
    public class RandomService : MonoSingleton<RandomService>
    {
        /// <summary>
        /// </summary>
        /// <param name="min">Inclusive</param>
        /// <param name="max">Inclusive</param>
        public int Generate(int min, int max)
            => Random.Range(min, max + 1);

        public T PickOne<T>(IEnumerable<T> collection)
            => PickMany(collection, 1).Single();

        public IEnumerable<T> PickMany<T>(IEnumerable<T> collection, int count) 
            => collection.OrderBy(_ => Guid.NewGuid()).Take(count);
    }
}