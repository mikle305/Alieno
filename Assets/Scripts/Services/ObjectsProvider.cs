using Additional.Game;
using Cinemachine;
using UI.GamePlay;
using UnityEngine;

namespace Services
{
    public class ObjectsProvider : MonoSingleton<ObjectsProvider>
    {
        [SerializeField] private Camera _uiCamera;
        
        private Camera _mainCamera;

        
        public Hud Hud { get; set; }
        public GameObject Character { get; set; }
        public GameObject Marker { get; set; }
        public Camera MainCamera { get; set; }
        public CinemachineVirtualCamera VirtualCamera { get; set; }
        public Camera UICamera => _uiCamera;
    }
}