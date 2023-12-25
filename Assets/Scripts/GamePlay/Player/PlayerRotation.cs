using Services;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _turnSpeed = 360f;

        private RadarService _radarService;
        private Transform _transform;


        private void Start()
        {
            _transform = transform;
            _radarService = RadarService.Instance;
        }

        public void Rotate(float deltaTime)
        {
            Transform target = _radarService.GetClosestFromPlayer();
            if (target == null)
                RotateToMovement(deltaTime);
            else
                RotateToTarget(target, deltaTime);
        }

        private void RotateToTarget(Transform target, float deltaTime)
        {
            Vector3 direction = target.position - _transform.position;
            RotateToDirection(direction, deltaTime);
        }

        private void RotateToMovement(float deltaTime)
        {
            Vector3 velocity = _rigidbody.velocity;
            if (velocity == Vector3.zero)
                return;

            RotateToDirection(velocity, deltaTime);
        }

        private void RotateToDirection(Vector3 direction, float deltaTime)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, _turnSpeed * deltaTime);
        }
    }
}