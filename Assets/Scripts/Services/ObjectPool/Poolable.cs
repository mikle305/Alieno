using Additional.Game;
using UnityEngine;

namespace Services
{
    public class Poolable : MonoBehaviour
    {
        private IObjectPool<GameObject> _pool;
        

        public void Init(IObjectPool<GameObject> pool) 
            => _pool = pool;

        public void Release()
            => _pool.Release(gameObject);
    }
}