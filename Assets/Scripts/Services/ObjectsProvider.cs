using Additional.Game;
using UnityEngine;

namespace Services
{
    public class ObjectsProvider : MonoSingleton<ObjectsProvider>
    {
        [SerializeField] private Camera _uiCamera;
        
        private Camera _mainCamera;

        
        public Camera UICamera => _uiCamera;
        public Camera MainCamera => _mainCamera ??= Camera.main;
    }
}