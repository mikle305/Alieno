using Additional.Game;
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
        public Camera UICamera => _uiCamera;
        public Camera MainCamera => _mainCamera ??= Camera.main;
    }
}