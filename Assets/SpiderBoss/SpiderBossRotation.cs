using System;
using Services;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class SpiderRotation : MonoBehaviour
    {
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private float _turnSpeed = 6f;

        public void UpdateRotation(Transform _target)
        {
            Quaternion OriginalRot = _enemyTransform.rotation;
            _enemyTransform.LookAt(_target);
        
            Quaternion NewRot = transform.rotation;
        
            transform.rotation = Quaternion.Lerp(OriginalRot, NewRot, _turnSpeed * Time.deltaTime);
        }

        private void Update()
        {
            GameObject character = ObjectsProvider.Instance.Character;
            if (character == null)
                return;
            
            UpdateRotation(character.transform);
        }

        public void UpdateRotation(Vector3 _target)
        {
            Quaternion OriginalRot = _enemyTransform.rotation;
            _enemyTransform.LookAt(_target);
        
            Quaternion NewRot = transform.rotation;
        
            transform.rotation = Quaternion.Lerp(OriginalRot, NewRot, _turnSpeed * Time.deltaTime);
        }
    }
}