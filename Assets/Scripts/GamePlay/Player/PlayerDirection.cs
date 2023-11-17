using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerDirection : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _turnSpeed = 0.5f;
        private Vector3 _mouse_pos;
        private Vector3 _playerPos;
        private float _angle;
    
        void Update()
        {
            UpdatePlayerDirection();
        }
    
        private void UpdatePlayerDirection()
        {
            GetAngle();

            SetAngleWithSmooth();
        }

        private void SetAngleWithSmooth()
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -_angle + 90, 0));

            _playerTransform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
        }

        private void GetAngle()
        {
            _mouse_pos = Input.mousePosition;
            _mouse_pos.z = 5.23f; //The distance between the camera and object
            _playerPos = _camera.WorldToScreenPoint(_playerTransform.position);
            _mouse_pos.x = _mouse_pos.x - _playerPos.x;
            _mouse_pos.y = _mouse_pos.y - _playerPos.y;
            _angle = Mathf.Atan2(_mouse_pos.y, _mouse_pos.x) * Mathf.Rad2Deg;
        }
    }
}
