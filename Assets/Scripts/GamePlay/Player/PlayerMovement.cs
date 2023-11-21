using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _body;
        [SerializeField] private float _runSpeed = 5.0f;


        public void Move(Vector2 direction) 
            => _body.velocity = new Vector3(x: direction.x * _runSpeed, y: 0, z: direction.y * _runSpeed);

        public void Stop()
        {
            _body.velocity = Vector3.zero;
        }
    }
}
