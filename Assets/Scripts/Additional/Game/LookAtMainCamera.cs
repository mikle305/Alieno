using Services;
using UnityEngine;
using VContainer;

namespace Additional.Game
{
    public class LookAtMainCamera : MonoBehaviour
    {
        private ObjectsProvider _objectsProvider;
        private Transform _transform;


        [Inject]
        public void Construct(ObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
            _transform = transform;
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