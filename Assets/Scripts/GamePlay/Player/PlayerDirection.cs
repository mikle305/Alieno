using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerDirection : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _turnSpeed = 0.5f;
        
        
        private void Update() 
            => UpdatePlayerDirection();

        private void UpdatePlayerDirection()
        {
            float angle = GetAngle();
            SetAngleSmoothly(angle);
        }

        private void SetAngleSmoothly(float angle)
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));
            _playerTransform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
        }

        private float GetAngle()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 playerPosition = _camera.WorldToScreenPoint(_playerTransform.position);

            mousePosition.x -= playerPosition.x;
            mousePosition.y -= playerPosition.y;
            mousePosition.z = 5.23f; //The distance between the camera and object

            return Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        }
    }
}
