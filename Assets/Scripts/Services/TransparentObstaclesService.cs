using System.Collections.Generic;
using Additional.Game;
using UnityEngine;

namespace Services
{
    public class TransparentObstaclesService : MonoSingleton<TransparentObstaclesService>
    {
        [SerializeField] private LayerMask _obstacleLayer;

        private ObjectsProvider _objectsProvider;
        private static readonly int _shaderParam = Shader.PropertyToID("_IsObstacleTransparent");
        private readonly RaycastHit[] _currentHits = new RaycastHit[30];
        private HashSet<Renderer> _previousTransparents = new();

        private void Start()
        {
            _objectsProvider = ObjectsProvider.Instance;
        }

        private void Update()
        {
            Camera mainCamera = _objectsProvider.MainCamera;
            GameObject character = _objectsProvider.Character;
            if (IsCameraOrPlayerDisabled(mainCamera, character))
                return;

            int raycastedCount = RaycastFromCameraNonAlloc(mainCamera, character);
            if (raycastedCount == 0)
            {
                SetAllPreviousOpaque();
                return;
            }
            
            HashSet<Renderer> currentTransparent = SetRaycastedTransparent(raycastedCount);
            SetOtherPreviousOpaque(currentTransparent);
        }

        private HashSet<Renderer> SetRaycastedTransparent(int raycastedCount)
        {
            var currentTransparents = new HashSet<Renderer>();
            for (var i = 0; i < raycastedCount; i++)
            {
                RaycastHit hit = _currentHits[i];
                
                if (!hit.collider.gameObject.TryGetComponent(out MeshRenderer obstacleRenderer))
                    continue;
                
                if (!_previousTransparents.Contains(obstacleRenderer))
                    SetShaderParam(obstacleRenderer, true);
                
                currentTransparents.Add(obstacleRenderer);
            }

            return currentTransparents;
        }

        private void SetOtherPreviousOpaque(HashSet<Renderer> currentTransparents)
        {
            foreach (Renderer previousTransparent in _previousTransparents)
            {
                if (currentTransparents.Contains(previousTransparent))
                    continue;
                
                SetShaderParam(previousTransparent, false);
            }

            _previousTransparents = currentTransparents;
        }

        private void SetAllPreviousOpaque()
        {
            foreach (Renderer obstacleRenderer in _previousTransparents) 
                SetShaderParam(obstacleRenderer, false);
            
            _previousTransparents.Clear();
        }

        private int RaycastFromCameraNonAlloc(Camera mainCamera, GameObject character)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 direction = (character.transform.position - cameraPosition).normalized;
            return Physics.RaycastNonAlloc(
                origin: cameraPosition,
                direction,
                _currentHits,
                maxDistance: 10f,
                layerMask: _obstacleLayer);
        }

        private static bool IsCameraOrPlayerDisabled(Camera mainCamera, GameObject character)
            => mainCamera == null || character == null || !character.activeSelf;

        private static void SetShaderParam(Renderer obstacleRenderer, bool isTransparent)
            => obstacleRenderer.material.SetFloat(_shaderParam, isTransparent ? 1.0f : 0.0f);
    }
}