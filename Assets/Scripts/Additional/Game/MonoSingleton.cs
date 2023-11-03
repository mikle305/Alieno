using Additional.Utils;
using UnityEngine;

namespace Additional.Game
{
    public class MonoSingleton<T> : MonoBehaviour
        where T : MonoSingleton<T>
    {
        private static T _instance;
        
        public static T Instance => _instance;
        
        
        protected virtual void Awake()
        {
            if (_instance != null)
                ThrowUtils.ManyInstancesOfSingleton();
                
            _instance = (T)this;
        }
    }
}