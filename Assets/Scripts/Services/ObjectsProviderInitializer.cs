using Additional.Game;
using Cinemachine;
using UnityEngine;

namespace Services
{
    public class ObjectsProviderInitializer : MonoSingleton<ObjectsProviderInitializer>
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        private ObjectsProvider _objectsProvider;


        private void Start()
        {
            _objectsProvider = ObjectsProvider.Instance;
            _objectsProvider.MainCamera = _mainCamera;
            _objectsProvider.VirtualCamera = _virtualCamera;
        }
    }
}