using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Projectile
{
    public class ProjectileMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        
        public bool IsWorking { get; set; }
        public Vector3 Direction { get; set; }
        public float Speed { get; set; }
        
        private void Update()
        {
            if (!IsWorking)
                 return;
            
            _rigidbody.velocity = Direction * Speed;
        }

        private void OnDisable() 
            => IsWorking = false;
    }
}