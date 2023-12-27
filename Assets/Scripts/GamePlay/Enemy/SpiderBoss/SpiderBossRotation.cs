using Services;
using UnityEngine;
using VContainer;

namespace GamePlay.Enemy
{
    public class SpiderRotation : MonoBehaviour
    {
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private float _turnSpeed = 6f;
        
        private ObjectsProvider _objectsProvider;


        [Inject]
        public void Construct(ObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
        }

        private void Update()
        {
            GameObject character = _objectsProvider.Character;
            if (character == null)
                return;
            
            UpdateRotation(character.transform);
        }

        private void UpdateRotation(Transform target)
        {
            Quaternion originalRot = _enemyTransform.rotation;
            _enemyTransform.LookAt(target);
            Quaternion newRot = transform.rotation;
            transform.rotation = Quaternion.Lerp(originalRot, newRot, _turnSpeed * Time.deltaTime);
        }
    }
}