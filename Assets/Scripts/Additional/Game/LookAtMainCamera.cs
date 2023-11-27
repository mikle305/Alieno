using Services;
using UnityEngine;

namespace Additional.Game
{
    public class LookAtMainCamera : MonoBehaviour
    {
        private ObjectsProvider _objectsProvider;
        private Transform _transform;
        
        
        private void Start()
        {
            _transform = transform;
            _objectsProvider = ObjectsProvider.Instance;
        }

        private void Update() 
            => LookAtCamera();

        private void LookAtCamera()
        {
            Vector3 cameraPosition = _objectsProvider.MainCamera.transform.position;
            _transform.rotation = Quaternion.LookRotation(_transform.position - cameraPosition);
        }
    }
}