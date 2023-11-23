using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationToEnemy : MonoBehaviour
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Rigidbody _playerRigidBody;
        [SerializeField] private float _turnSpeed = 10f;

        private RadarService _radarService;
        private void Start()
        {
            _radarService = RadarService.Instance;
        }

        private void Update() 
            => UpdateRotation(_radarService.GetClosestAndVisibleEnemy());

        public void UpdateRotation(Transform _target)
        {
            if(_target == null)
                RotateToMovement();

            RotateToEnemy(_target);
        }

        private void RotateToEnemy(Transform _target)
        {
            Quaternion OriginalRot = _playerTransform.rotation;
            _playerTransform.LookAt(_target);

            Quaternion NewRot = transform.rotation;

            transform.rotation = Quaternion.Lerp(OriginalRot, NewRot, _turnSpeed * Time.deltaTime);
        }

        private void RotateToMovement()
        {
            var velocity = _playerRigidBody.velocity;
            
            if(velocity != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(velocity, transform.up);

                transform.rotation = Quaternion.RotateTowards(_playerTransform.rotation, toRotation, _turnSpeed * Time.deltaTime);
            }
        }
    }
}
