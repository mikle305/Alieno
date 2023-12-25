using System;
using UnityEngine;

namespace GamePlay.Test
{
    public class TestPlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _body;
        [SerializeField] private float _runSpeed = 5.0f;
        
        private Vector2 _movementDirection;


        private void Update()
        {
            _movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        private void FixedUpdate()
        {
            if (_movementDirection != Vector2.zero)
                Move(_movementDirection);
            else
                Stop();
        }

        private void Move(Vector2 direction) 
            => _body.velocity = new Vector3(x: direction.x * _runSpeed, y: 0, z: direction.y * _runSpeed);

        private void Stop() 
            => _body.velocity = Vector3.zero;
    }
}