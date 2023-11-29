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

        /// <summary>
        /// </summary>
        /// <param name="min">Inclusive</param>
        /// <param name="max">Inclusive</param>
        public float Generate(float min, float max)
            => Random.Range(min, max);
        
        /// <summary>
        /// </summary>
        /// <param name="chance">Chance coefficient from 0 to 1</param>
        /// <returns></returns>
        public bool TryChance(float chance)
        {
            if (chance < 0 || chance > 1)
                throw new InvalidOperationException("InvalidChance");
            
            if (chance == 0)
                return false;
            
            return chance >= Generate(0.0f, 1.0f);
        }

        public T PickOne<T>(IEnumerable<T> collection)
            => PickMany(collection, 1).Single();

        public IEnumerable<T> PickMany<T>(IEnumerable<T> collection, int count) 
            => collection.OrderBy(_ => Guid.NewGuid()).Take(count);
    }
}