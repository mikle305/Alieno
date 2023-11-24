using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Projectile
{
    public class ProjectileMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        private bool _isWorking;


        public void StartMove(Vector3 direction, float speed)
            => MoveAsync(direction, speed).Forget();
        
        private async UniTask MoveAsync(Vector3 direction, float speed)
        {
            _isWorking = true;
            while (_isWorking)
            {
                _rigidbody.velocity = direction * speed;
                await UniTask.Yield();
            }
        }

        private void OnDisable()
        {
            _isWorking = false;
        }
    }
}