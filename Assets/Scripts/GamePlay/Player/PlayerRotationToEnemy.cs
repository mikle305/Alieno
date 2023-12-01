    using Services;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerRotationToEnemy : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Rigidbody _playerRigidBody;
        [SerializeField] private float _turnSpeed = 10f;
        [SerializeField] private PlayerDash _playerDash;
    
        private RadarService _radarService;

    
        private void Start()
        {
            _radarService = RadarService.Instance;
        }

        private void Update()
            => UpdateRotation(_radarService.GetClosestAndVisibleEnemy());

        private void UpdateRotation(Transform target)
        {
            if (_playerDash.IsDashing)
                return;
            
            if (target == null)
                RotateToMovement();

            RotateToEnemy(target);
        }

        private void RotateToEnemy(Transform target)
        {
            Quaternion originalRotation = _playerTransform.rotation;
            _playerTransform.LookAt(target);
            Quaternion newRotation = transform.rotation;
            // newRotation.x = originalRotation.x;
            // newRotation.z = originalRotation.z;
            transform.rotation = Quaternion.Lerp(originalRotation, newRotation, _turnSpeed * Time.deltaTime);
        }

        private void RotateToMovement()
        {
            Vector3 velocity = _playerRigidBody.velocity;
            if (velocity == Vector3.zero) 
                return;

            Quaternion toRotation = Quaternion.LookRotation(velocity, transform.up);
            // toRotation.x = _playerTransform.rotation.x;
            // toRotation.z = _playerTransform.rotation.z;
            _playerTransform.rotation =
                Quaternion.RotateTowards(_playerTransform.rotation, toRotation, _turnSpeed * Time.deltaTime);
        }
    }
}