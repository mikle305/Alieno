using Additional.Game;
using UnityEngine;

namespace Services
{
    public class Poolable : MonoBehaviour
    {
        private IObjectPool<Poolable> _pool;
        

        public void Init(IObjectPool<Poolable> pool) 
            => _pool = pool;

        public void Release()
            => _pool.Release(this);
    }
}