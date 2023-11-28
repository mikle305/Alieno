using System.Threading;
using Additional.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Projectile
{
    public class ProjectileLifetime : MonoBehaviour
    {
        [SerializeField] private float _duration = 10;

        private CancellationTokenSource _tokenSource;
        

        public void Init()
        {
            _tokenSource = new CancellationTokenSource();
            DisposeAsync(_tokenSource.Token).Forget();
        }

        private async UniTask DisposeAsync(CancellationToken token)
        {
            await UniTask.WaitForSeconds(_duration, cancellationToken: token);
            this.Dispose();
        }

        private void OnDisable() 
            => _tokenSource?.Cancel();
    }
}